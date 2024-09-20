using MediatR;
using Postech.GroupEight.ContactFind.Application.Commands.Inputs;
using Postech.GroupEight.ContactFind.Application.Commands.Outputs;
using Postech.GroupEight.ContactFind.Application.Handlers.Contacts;
using Postech.GroupEight.ContactFind.Application.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Api.Setup
{
    [ExcludeFromCodeCoverage]
    public static class HandlerSetup
    {
        /// <summary>
        /// Adds the dependency handler to the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void AddDependencyHandler(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddScoped<IRequestHandler<FindContactInput, DefaultOutput<IEnumerable<FindContactByAreaCodeViewModel>>>, FindContactByAreaCodeHandler>();
        }
    }
}
