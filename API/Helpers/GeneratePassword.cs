using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class GeneratePassword
    {

        public static string GenerateRandomPassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            var charsForPassword = new char[50];
            var random = new Random();

            for (int i = 0; i < charsForPassword.Length; i++)
            {
                charsForPassword[i] = chars[random.Next(chars.Length)];
            }


            return new String(charsForPassword);
        }
    }
}