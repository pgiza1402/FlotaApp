using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UserController: BaseApiController
    {
        public UserManager<AppUser> _userManager { get; }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;

        }

        //[Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                    .Include(r => r.UserRoles)
                    .ThenInclude(r => r.Role)
                    .OrderBy(u => u.UserName)
                    .Select(u => new
                    {
                        u.Id,
                        UserName = u.UserName,
                        DisplayName = u.DisplayName,
                        u.CarId,
                        u.Email,
                        u.PhoneNumber,
                        Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                    })
                    .ToListAsync();

            return Ok(users);
        }


        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound("Nie udało się znaleźć użytkownika");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return BadRequest("Nie udało się dodać roli");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return BadRequest("Nie udało się usunąć roli");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(UserDtoUpdate userDtoUpdate, string id)
        {
            var login = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var appUser = await _userManager.Users.Where(x => x.UserName == login).FirstOrDefaultAsync();
            var user = await _userManager.FindByIdAsync(id);

            if(user != null){

                 await _unitOfWork.LogRepository.AddUserLogAsync(appUser.DisplayName, user, userDtoUpdate);
            }

            if (userDtoUpdate.Password != null && userDtoUpdate.Password != "") 
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, userDtoUpdate.Password);
            }

            if (userDtoUpdate.UserName != user.UserName)
            {
                if (await _userManager.FindByNameAsync(userDtoUpdate.UserName) != null) return BadRequest("Login jest używany przez innego użytkownika");

                user.UserName = userDtoUpdate.UserName;
                user.DisplayName = userDtoUpdate.UserName;
                user.Email = userDtoUpdate.Email;
                user.PhoneNumber = userDtoUpdate.PhoneNumber;

                await _userManager.UpdateAsync(user);
            } else if (userDtoUpdate.Email != user.Email || userDtoUpdate.PhoneNumber != user.PhoneNumber)
            {
                user.Email = userDtoUpdate.Email;
                user.PhoneNumber = userDtoUpdate.PhoneNumber;

                await _userManager.UpdateAsync(user);
            }

            var userRoles = _userManager.GetRolesAsync(user);

            if (!userRoles.Result.Contains(userDtoUpdate.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, userRoles.Result);
                await _userManager.AddToRoleAsync(user, userDtoUpdate.Role);
            }
            return Ok();


        }

        [HttpPost("addUser")]
        public async Task<ActionResult> AddUser(UserDtoUpdate userDto)
        {
            
            userDto.Password = userDto.Password == null ? GeneratePassword.GenerateRandomPassword() : userDto.Password;
            var validCredensials = AD.CredensialsValidation(userDto.UserName, userDto.Password);
            var userExist = AD.UserExist(userDto.UserName, _userManager);
            if (await userExist) return BadRequest("Wpisany login jest zajęty");

            var user = new AppUser
            {
                UserName = userDto.UserName.ToLower(),
                DisplayName = AD.GetDisplayNameByLogin(userDto.UserName),
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber

            };
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            var roleResult = await _userManager.AddToRoleAsync(user, userDto.Role);

            if (!roleResult.Succeeded) return BadRequest(result.Errors);


            return Ok();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id){

            var user = await _userManager.Users.Where(x => x.Id == id)
             .Include(r => r.UserRoles)
                        .ThenInclude(r => r.Role)
                        .Select(u => new {
                            u.Id,
                            UserName = u.UserName,
                            DisplayName = u.DisplayName,
                            u.CarId,
                            u.Email,
                            u.PhoneNumber,
                            Roles = u.UserRoles.Select(r => r.Role.Name).First()
                        })
                        .FirstOrDefaultAsync();

            if(user == null) return BadRequest("Brak użytkownika o wybranym id");
            

            return Ok(user);

            
        }

        [HttpGet("usernametaken")]
        public async Task<ActionResult<bool>> CheckUserExistAsync([FromQuery]string userName){

             var userExists = await _userManager.FindByNameAsync(userName);

            if(userExists == null){
                return false;
             } 
             else{
                 return true;
             }


        }




    }
}