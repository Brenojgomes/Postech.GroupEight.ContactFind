﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using Postech.GroupEight.ContactFind.Core.Interfaces.Repositories;
using Postech.GroupEight.ContactFind.Infra.Repositories;

namespace Postech.GroupEight.ContactFind.Infra
{
    public static class InfrastuctureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMongo();
            services.AddRepositories();
            return services;
        }

        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton<MongoDbOptions>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var options = new MongoDbOptions();
                configuration.GetSection("Mongo").Bind(options);
                return options;
            });

            services.AddSingleton<IMongoClient>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var options = sp.GetService<MongoDbOptions>();
                var client = new MongoClient(options.ConnectionString);
                return client;
            });

            services.AddTransient(sp =>
            {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
                var client = sp.GetService<IMongoClient>();
                var options = sp.GetService<MongoDbOptions>();
                return client.GetDatabase(options.Database);
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IContactRepository, ContactRepository>();
            return services;
        }
    }
}
