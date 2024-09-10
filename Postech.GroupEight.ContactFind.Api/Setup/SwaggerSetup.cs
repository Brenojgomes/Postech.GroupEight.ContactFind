using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class SwaggerSetup
    {
        /// <summary>
        /// Configures Swagger generation for the API.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddSwaggerGenConfiguration(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ContactFind API (Tech Challenge)",
                    Version = "v1",
                    Description = "Alunos responsáveis: Breno Gomes (RM353570), Lucas Pinho (RM356299), Lucas Ruiz (RM353388), Ricardo Fulgencio (RM354423) e Tatiana Lima (RM353457)",
                    Contact = new OpenApiContact()
                    {
                        Name = "Grupo 8 (Tech Challenge)",
                        Url = new Uri("https://bit.ly/3xCPMQ4")
                    }
                });
                c.EnableAnnotations();
            });
        }
    }
}
