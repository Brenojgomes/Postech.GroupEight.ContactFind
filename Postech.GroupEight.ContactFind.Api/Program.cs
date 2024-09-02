using MediatR;
using Postech.GroupEight.ContactFind.Application.Commands.Inputs;
using Postech.GroupEight.ContactFind.Core.Exceptions.Common;
using Postech.GroupEight.ContactFind.Application.Extensions;
using Postech.GroupEight.ContactFind.Infra;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



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

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
