using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace ApiIntegrationsTests
{
    public class CarControllerTest
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        public CarControllerTest()
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

        [Theory]
        [InlineData("car")]
        public async void GetAll_ReturnOkResult(string url)
        {

          //act

          var response = await _client.GetAsync("/api/" + url);

          response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }

        [Fact]
        public async Task CreateCar_WithValidModel_ReturnsOk()
        {
          //arange

          var service = new ServiceDto(){
            ServiceExpirationDate = DateTime.Now,
            NextServiceMeterStatus = 123450
          };

          var model = new CarDto()
          {
            Brand = "Audi",
            Model = "A4",
            RegistrationNumber = "KT12345",
            MeterStatus = "12345",
            VAT = "100%",
            TechnicalExaminationExpirationDate = DateTime.Now,
            Service = service,
            Year = 2012
            

          };
          //act
          var response = await _client.PostAsJsonAsync("api/car", model);
          //assert
          response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
          response.Headers.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ForCar_ReturnsNoContent()
        {
          // arrange
          

          var car = new Car()
          {
            Brand = "Audi",
            Model = "A4",
            RegistrationNumber = "KT12345",
            MeterStatus = 12345,
            VAT = "100%",
            Year = 2012
          };


          var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
          using var scope = scopeFactory.CreateScope();
          var _dbContext = scope.ServiceProvider.GetService<DataContext>();

           _dbContext.Cars.Add(car);
          await _dbContext.SaveChangesAsync();
          
          var response = await _client.DeleteAsync("api/car/" + car.Id);


          //assert
          response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        }
      

        [Fact]
        public async Task Delete_ForNonExistingCar_ReturnsNotFound()
        {
          // act

           var response = await _client.DeleteAsync("/api/car/" + 987);
        
          // assert

          response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

         
        }

        // [Fact]
        // public async Task CreateCar_WithInvalidModel_Returns()
        // {
        //   //arrange

        //   var model = new CarDto()
        //   {
        //      Brand = "Audi",
        //     Model = "A4",
        //     RegistrationNumber = "KT12345",
        //     MeterStatus = "12345",
        //     VAT = "100%",
        //     TechnicalExaminationExpirationDate = DateTime.Now,
        //     Year = 2012
        //   };

        //   var response = await _client.PostAsJsonAsync("api/car", model);
        
        //   //assert

        //   response.StatusCode.Should().Be(System.Net.HttpStatusCode)
        // }
    }
}