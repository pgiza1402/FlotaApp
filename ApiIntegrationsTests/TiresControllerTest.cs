using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ApiIntegrationsTests
{
    public class TiresControllerTest
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;
        public TiresControllerTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DataContext>));

                    services.Remove(dbContextOptions);

                    services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("FlotaDb"));
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));
                });
            });

            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ForTires_ReturnsOk()
        {
            var response = await _client.GetAsync("api/tires");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_ForTires_ReturnOk()
        {
            var model = new Tires()
            {
                Model = "Continental",
                Quantity = 4,
                Type = "letnie",
                StoragePlace = "Kontener",
                 Car = new Car()
                    {
                    Brand = "Audi",
                    Model = "A4",
                    RegistrationNumber = "KT12345",
                    MeterStatus = 12345,
                    VAT = "100%",
                    Year = 2012   
                    },
            };

               var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<DataContext>();

            _dbContext.Tires.Add(model);
            await _dbContext.SaveChangesAsync();
            

            var response = await _client.GetAsync("api/tires/" + model.Id);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Add_ForTires_ReturnsOk()
        {
             var model = new Tires()
            {
                Model = "Continental",
                Quantity = 4,
                Type = "letnie",
                StoragePlace = "Kontener",
                 Car = new Car()
                    {
                    Brand = "Audi",
                    Model = "A4",
                    RegistrationNumber = "KT12345",
                    MeterStatus = 12345,
                    VAT = "100%",
                    Year = 2012   
                    },
            };
           
           var response = await _client.PostAsJsonAsync("api/tires", model);

           response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Update_ForTires_ReturnsOk()
        {
              var model = new Tires()
            {
                Model = "Continental",
                Quantity = 4,
                Type = "letnie",
                StoragePlace = "Kontener",
                 Car = new Car()
                    {
                    Brand = "Audi",
                    Model = "A4",
                    RegistrationNumber = "KT12345",
                    MeterStatus = 12345,
                    VAT = "100%",
                    Year = 2012   
                    },
            };

               var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<DataContext>();

            _dbContext.Tires.Add(model);
            await _dbContext.SaveChangesAsync();

              var update = new Tires()
            {
                Model = "Continental",
                Quantity = 4,
                Type = "letnie",
                StoragePlace = "Kontener",
                 Car = new Car()
                    {
                    Brand = "Audi",
                    Model = "A8",
                    RegistrationNumber = "KT12345",
                    MeterStatus = 12345,
                    VAT = "50%",
                    Year = 2012   
                    },
            };

            var response = await _client.PutAsJsonAsync("api/tires/" + model.Id, update);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}