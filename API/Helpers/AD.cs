using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class AD
    {

        //Metoda sprawdzająca poprawność wprowadzonych danych logowania do domeny
        public static bool CredensialsValidation(string username, string password)
        {
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    return context.ValidateCredentials(username, password);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //Metoda sprawdzająca czy użytkownik jest już w bazie danych
        public static async Task<bool> UserExist(string login, UserManager<AppUser> userManager)
        {
            return await userManager.Users.AnyAsync(x => x.UserName == login.ToLower());
        }

        //Metoda wyciągająca Imię i Nazwisko użytkownika z domeny na podstawie loginu
        public static string GetDisplayNameByLogin(string login)
        {
            PrincipalContext context;
            UserPrincipal user;
            try
            {
                using (context = new PrincipalContext(ContextType.Domain))
                {
                    user = UserPrincipal.FindByIdentity(context, login);
                    if (user == null) return login;
                    else return user.DisplayName;
                }
            }
            catch (Exception ex)
            {
                return login;
            }
        }
    }
}