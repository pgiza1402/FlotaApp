using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        //  public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var validCredensials = AD.CredensialsValidation(loginDto.UserName, loginDto.Password);
            var userExist = AD.UserExist(loginDto.UserName, _userManager);
           // var foundedUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            //if(foundedUser != null && )
            //Sprawdzanie czy użytkownik wprowadził poprawne dane do domeny i czy nie istnieje w bazie
            if (validCredensials == true && await userExist == false)
            {
                var user = new AppUser
                {
                    UserName = loginDto.UserName.ToLower(),
                    DisplayName = AD.GetDisplayNameByLogin(loginDto.UserName)

                };
                var result = await _userManager.CreateAsync(user, loginDto.Password);
                if (!result.Succeeded) return BadRequest(result.Errors);
                var roleResult = await _userManager.AddToRoleAsync(user, "Nieaktywowany");

                if (!roleResult.Succeeded) return BadRequest(result.Errors);
                return new UserDto
                {
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    CarId = user?.CarId,
                    Token = await _tokenService.CreateToken(user)
                };
            }
            //Sprawdzanie czy użytkownik wprowadził poprawne dane do domeny i czy istnieje w bazie i czy hasło w bazie jest aktualne
            else if (validCredensials == true && await userExist == true)
            {
                var foundedUser = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

                if (await _userManager.CheckPasswordAsync(foundedUser, loginDto.Password) == false)
                {
                    await _userManager.RemovePasswordAsync(foundedUser);

                    await _userManager.AddPasswordAsync(foundedUser, loginDto.Password);
                }
                var result = await _signInManager.CheckPasswordSignInAsync(foundedUser, loginDto.Password, false);

                if (!result.Succeeded) return BadRequest();
                return new UserDto
                {
                    UserName = foundedUser.UserName,
                    CarId = foundedUser?.CarId,
                    DisplayName = foundedUser.DisplayName,
                    Token = await _tokenService.CreateToken(foundedUser)
                };
            }
            else if (validCredensials == false && await userExist == true)
            {
                var foundedUser = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

                var result = await _signInManager.CheckPasswordSignInAsync(foundedUser, loginDto.Password, false);

                if (!result.Succeeded) return BadRequest("Nieprawidłowy login lub hasło");

                return new UserDto
                {
                    UserName = foundedUser.UserName,
                    CarId = foundedUser?.CarId,
                    DisplayName = foundedUser.DisplayName,
                    Token = await _tokenService.CreateToken(foundedUser)
                };
            }
            else
            {
                return BadRequest("Nieprawidłowy login lub hasło");
            }
        }
    }
}