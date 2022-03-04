using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ApiIntegrationsTests
{
    public class FieldControllerTest
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;
        public FieldControllerTest()
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
    public async Task GetAllFields_ReturnsOkResult()
    {
        //act

        var response = await _client.GetAsync("api/field");

        //assert

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        
    }

    [Fact]
    public async Task CreateField_withValidModel_ReturnOk()
    {
       //arrange
       var model = new FieldDto()
       {
           Name  = "2",
           Category = "Ilość opon"
       };

       var response = await _client.PostAsJsonAsync("api/field", model);
       
       response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

    }

    [Fact]
    public async Task Delete_ForField_ReturnsNoContent()
    {
       var model = new Field()
       {
           Name  = "2",
           Category = "Ilość opon"
       };  

       var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();

       using var scope = scopeFactory.CreateScope();
       var _dbContext = scope.ServiceProvider.GetService<DataContext>();

       _dbContext.Fields.Add(model);
       await _dbContext.SaveChangesAsync();

       var response = await _client.DeleteAsync("api/field/" + model.Id);

       response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
       
    }

    [Fact]
    public async Task Delete_ForFieldWithNotExistingId_ReturnsBadRequest()
    {
       var response = await _client.DeleteAsync("api/field/" + 987);
       

       response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_ForField_ReturnsOk()
    {
       var model = new Field()
       {
           Name  = "2",
           Category = "Ilość opon"
       };  

       var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();

       using var scope = scopeFactory.CreateScope();
       var _dbContext = scope.ServiceProvider.GetService<DataContext>();

       _dbContext.Fields.Add(model);
       await _dbContext.SaveChangesAsync();

       var updateModel = new Field()
       {
           Name = "3",
           Category = "Ilość opon"
       };

       var response = await _client.PutAsJsonAsync("api/field/" + model.Id, updateModel);

       response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}
}