using Xunit;
using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {
            
        }
    }

    public class DbTest : IDisposable
    {
        public string databaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider {get; private set;}

        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o =>
                o.UseMySql($"Server=localhost;Port=5003;Database={databaseName};Uid=sa;Pwd=S@geBr.2014"),
                ServiceLifetime.Transient
            );

            ServiceProvider = serviceCollection.BuildServiceProvider();
            using(var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using(var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
