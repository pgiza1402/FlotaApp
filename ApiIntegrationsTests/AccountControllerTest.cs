using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace ApiIntegrationsTests
{
    public class AccountControllerTest
    {
        private HttpClient _client;
        private Mock<ITokenService> _tokenServiceMock = new Mock<ITokenService>();
        private UserManager<AppUser>_userManager; //= new UserManager<AppUser>();
        private WebApplicationFactory<Program> _factory;

        public AccountControllerTest()
        {
        //    _userManager = userManager;
            var factory = new WebApplicationFactory<Program>();
            _factory = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DataContext>));

                    services.Remove(dbContextOptions);

                    services.AddSingleton<ITokenService>(_tokenServiceMock.Object);

                    services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("FlotaDb"));
                });
            });

            _client = _factory.CreateClient();

        }

    //     [Fact]
    //     public async Task LoginUser_ForValidModel_ReturnsOk()
    //     {
    //         var store = new Mock<IUserStore<AppUser>>();
    //         var _userManager = new UserManager<AppUser>(store.Object, null, null, null, null, null, null, null, null);
    //         var appUser = new AppUser()

           
    //         {
    //             UserName ="dbadmin"
    //         };

    //        // var password = "Pa$$w0rd";

    //          var response =  _userManager.CreateAsync(appUser).Result;


            
    //   //   var response =  _userManager.Setup(s => s.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).Returns(new Task<IdentityResult>(() => IdentityResult.Success));
    //    // var response = await _userManager.CreateAsync(appUser,password);

    //     IdentityResult result = null;;

    //     if(response.Succeeded)
    //     {
    //      result = IdentityResult.Success;
    //     };

    //     result.Should().Be(IdentityResult.Success);
        
        
          
    //         // arrange
    //     //   _tokenServiceMock
    //      //  .Setup(e => e.CreateToken(It.IsAny<AppUser>())).Returns(new Task<string>(() => "jwt"));
           
    //     // response.Should().Be(IdentityResult.Success);
          
    //     }
    }
}