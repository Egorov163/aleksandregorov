using System;

namespace game_guess_the_number
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var gameBuilder = new GameRuleBuilder();
            var gameRule = gameBuilder.AskUserGame();

            var game = new GuessNumberGame(gameRule);
            game.Play();
        }
    }
}
