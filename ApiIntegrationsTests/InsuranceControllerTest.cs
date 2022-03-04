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
    public class InsuranceControllerTest
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;
        public InsuranceControllerTest()
        {
         var factory = new WebApplicationFactory<Program>();
          _factory = factory 
          .WithWebHostBuilder(builder => {
            builder.ConfigureServices(services => {
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
        public async Task GetAll_ForInsurance_ReturnsOk()
        {
            var response = await _client.GetAsync("api/insurance");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Update_ForInsurance_ReturnsOk()
        {
          var model = new CarInsurance()
          {
            Insurer = "PZU",
            ExpirationDate = DateTime.Now,
            Package = "test package",
            Car = new Car()
                {
                Brand = "Audi",
                Model = "A4",
                RegistrationNumber = "KT12345",
                MeterStatus = 12345,
                VAT = "100%",
                Year = 2012   
                }
          };

             var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<DataContext>();

            _dbContext.CarInsurances.Add(model);
            await _dbContext.SaveChangesAsync();

          var updateModel = new CarInsurance()
           {
            Insurer = "UNIQUA",
            ExpirationDate = DateTime.Now,
            Package = "test package",
            Car = new Car()
                {
                Brand = "Audi",
                Model = "A4",
                RegistrationNumber = "KT12345",
                MeterStatus = 12345,
                VAT = "100%",
                Year = 2012   
                }
          };

          var response = await _client.PutAsJsonAsync("api/insurance/" + model.Id, updateModel);

          response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);



        }

        [Fact]
        public async Task GetById_ForInsurance_ReturnsOk()
        {

           var model = new CarInsurance()
          {
            Insurer = "PZU",
            ExpirationDate = DateTime.Now,
            Package = "test package",
            Car = new Car()
                {
                Brand = "Audi",
                Model = "A4",
                RegistrationNumber = "KT12345",
                MeterStatus = 12345,
                VAT = "100%",
                Year = 2012   
                }
          };

             var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<DataContext>();

            _dbContext.CarInsurances.Add(model);
            await _dbContext.SaveChangesAsync();

          var response = await _client.GetAsync("api/insurance/" + model.Id);

          //assert
          response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
          // Given
        
          // When
        
          // Then
        }
    }
}