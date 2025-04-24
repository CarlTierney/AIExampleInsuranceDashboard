using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using System;

namespace Domain
{
    public abstract class DesignDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
    
        private IConfiguration Configuration {get; }
        private string ConfigKey {get;}
        
        public DesignDbContextFactory(string configKey) {
            this.ConfigKey = configKey ?? throw new ArgumentNullException(nameof(configKey));
            ConfigurationBuilder cb = new ConfigurationBuilder();
            cb.AddUserSecrets<T>()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = cb.Build();

         
        }
        
        
        public T CreateDbContext(string[] args)
        {
            
            var builder = new DbContextOptionsBuilder<T>();
            var connString =  Configuration[ConfigKey];
            
            if (string.IsNullOrEmpty(connString))
            {
                throw new InvalidOperationException($"Connection string '{ConfigKey}' not found in configuration.");
            }
            builder.UseSqlServer(connString);
            return CreateDbContext(builder.Options);
        }
        
        protected abstract T CreateDbContext(DbContextOptions<T> options);
    }


    public class InsuranceDbContextFactory : DesignDbContextFactory<InsuranceDbContext>
    {
        public InsuranceDbContextFactory() : base("aitest") { }

        protected override InsuranceDbContext CreateDbContext(DbContextOptions<InsuranceDbContext> options)
        {
            Console.WriteLine("Creating InsuranceDbContext with provided options.");

            return new InsuranceDbContext(options);
        }
    }
}