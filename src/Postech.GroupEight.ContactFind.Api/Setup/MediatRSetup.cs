using System.Diagnostics.CodeAnalysis;

namespace Postech.GroupEight.ContactFind.Api.Setup
{
    [ExcludeFromCodeCoverage]
    public static class MediatRSetup
    {
        /// <summary>
        /// Adds MediatR services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        public static void AddMediatR(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });
        }
    }
}
