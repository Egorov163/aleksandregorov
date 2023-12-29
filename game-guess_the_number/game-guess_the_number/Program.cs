using System;

namespace game_guess_the_number
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var number = 10;
            int attempt = 10;

            Console.WriteLine("It's game: Guess the number");
            
            int userGuess;
            bool isUserGood;

            for (int i = 0; i < attempt; i++)
            {
                Console.WriteLine("Enter eny number");
                Console.WriteLine($"Attempt {i}/{attempt}");
                do
                {
                    var userNumberString = Console.ReadLine();
                    isUserGood = int.TryParse(userNumberString, out userGuess);
                    if (!isUserGood)
                    {
                        Console.WriteLine("It's not a number, enter NUMBER!");
                    }

                } while (!isUserGood);

                if (userGuess == number)
                {
                    Console.WriteLine("Win!");
                    return;
                }
                           
            }
            Console.WriteLine("Loser");
        }
    }
}
