using NextPassword.MVVM._utils.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NextPassword.MVVM._utils
{
    public class Generation
    {
        public Generation() { }

        public static string GenerateRandomPassword(int passwordLengthTarget = 16)
        {

            if (passwordLengthTarget < 12)
            {
                return null;
            }

            // Create all characters that can be used
            string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            string digits = "0123456789";
            string specialCharacters = "!@#$%^&*()_+{}|<>?";
            string allCharacters = $"{uppercaseLetters}{lowercaseLetters}{digits}{specialCharacters}";

            // Quotient and remainder recovery
            int quotient = passwordLengthTarget / 4;
            int remainder = passwordLengthTarget % 4;

            // Random object created
            Random random = new Random();   
            // Build final password
            StringBuilder stringBuilder = new StringBuilder();

            // Inserting each type of random character 
            for (int index = 0; index < quotient; index++)
            {
                stringBuilder.Append(uppercaseLetters[random.Next(uppercaseLetters.Length)]);
                stringBuilder.Append(lowercaseLetters[random.Next(lowercaseLetters.Length)]);
                stringBuilder.Append(digits[random.Next(digits.Length)]);
                stringBuilder.Append(specialCharacters[random.Next(specialCharacters.Length)]);
            }

            // Inserting random characters as a function of the quotient 
            if (remainder > 0)
            {
                for (int remainderIndex = 0; remainderIndex < remainder; remainderIndex++)
                {
                    stringBuilder.Append(allCharacters[random.Next(allCharacters.Length)]);
                }
            }

            // Build final password shuffled
            string finalRandomPassword = new string(stringBuilder.ToString().ToCharArray().OrderBy(s => (random.Next(2) % 2) == 0).ToArray());


            return finalRandomPassword;
        }
    }
}
