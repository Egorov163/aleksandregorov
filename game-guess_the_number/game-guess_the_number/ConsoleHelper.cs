using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_guess_the_number
{
    public class ConsoleHelper
    {
        public static int GetNumberFromUser()
        {
            bool isUserGood;
            int userGuess;
            do
            {
                var userNumberString = Console.ReadLine();
                isUserGood = int.TryParse(userNumberString, out userGuess);
                if (!isUserGood)
                {
                    Console.WriteLine("It's not a number. Enter NUMBER");
                }
            } while (!isUserGood);

            return userGuess;
        }
        public static string CheckingIncomingData(string rightAnswer1, string rightAnswer2)
        {
            bool isUserGood = false;
            string userGuess;
            do
            {
                userGuess = Console.ReadLine();

                if (userGuess == rightAnswer1 || userGuess == rightAnswer2)
                {
                    isUserGood = true;                   
                }
                else
                {
                    Console.WriteLine("Incorrect data, try again");
                }
                
            } while (!isUserGood);

            return userGuess;
        }
    }
}
