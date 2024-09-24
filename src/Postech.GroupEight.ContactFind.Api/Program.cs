using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Postech.GroupEight.ContactFind.Api.Setup;
using Postech.GroupEight.ContactFind.Application.Commands.Inputs;
using Postech.GroupEight.ContactFind.Application.Commands.Outputs;
using Postech.GroupEight.ContactFind.Application.Extensions;
using Postech.GroupEight.ContactFind.Core.Exceptions.Common;
using Postech.GroupEight.ContactFind.Infra;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenConfiguration();
builder.Services.AddMediatR();
builder.Services.AddDependencyHandler();
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}

app.UseHttpsRedirection();

app.UseExceptionHandler(configure =>
{
    configure.Run(async context =>
    {
        IExceptionHandlerPathFeature? exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error is not null)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string errorMessage = exceptionHandlerPathFeature.Error.Message;
            if (exceptionHandlerPathFeature?.Error is DomainException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exceptionHandlerPathFeature?.Error is NotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                errorMessage = "An unexpected error occurred";
            }
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new DefaultOutput(false, errorMessage));
        }
    });
});

app.UseHttpMetrics();

app.MapGet("/contacts", async (IMediator mediator, [AsParameters] FindContactInput request) =>
{
    DomainException.ThrowWhenThereAreErrorMessages(request.Validate());
    return await mediator.Send(request);
})
.WithName("Find Contact")
.WithMetadata(new SwaggerOperationAttribute("Find contacts by area code", "Returns registered contacts based on area code"))
.WithMetadata(new SwaggerParameterAttribute("Data to find contacts based on area code"))
.WithMetadata(new SwaggerResponseAttribute(200, "Contacts were found successfully"))
.WithMetadata(new SwaggerResponseAttribute(400, "The data provided to find the contacts is invalid"))
.WithMetadata(new SwaggerResponseAttribute(404, "No contacts found based on the area code provided"))
.WithMetadata(new SwaggerResponseAttribute(500, "Unexpected error while finding the contacts"))
.WithOpenApi();

app.Run("http://+:5266");

[ExcludeFromCodeCoverage]
public partial class Program
{
    protected Program() { }
}