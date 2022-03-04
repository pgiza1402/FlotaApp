using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ApiIntegrationsTests
{
    public class ProgramTest
    {
        private WebApplicationFactory<Program> _factory;
        private List<Type> _controllerTypes;

        public ProgramTest()
        {
            _controllerTypes = typeof(Program).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(ControllerBase))).ToList();
            var factory = new WebApplicationFactory<Program>();
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
              {
                  _controllerTypes.ForEach(c => services.AddScoped(c));
              });
            });
        }

        [Fact]
        public void ConfigureServices_ForControllers_RegistersAllDependencies()
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            //assert
            _controllerTypes.ForEach(t =>
            {
                var controller = scope.ServiceProvider.GetService(t);
                controller.Should().NotBeNull();
            });

        }

        
    }
}