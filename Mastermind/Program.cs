using System;

namespace Mastermind
{
    public class Program
    {
        private static bool _gameStart = false;
        private static bool _gameStop = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Greetings!");

            var answer = string.Join("", Answer.GenerateRandomAnswer());

            var game = new Game(answer);

            game.PrintRules();

            while (!_gameStart && !_gameStop)
            {
                
                game.PrintCommands();

                Console.Write("\n> ");
                var command = Console.ReadLine();

                if (!string.IsNullOrEmpty(command))
                {
                    switch (command.ToLower())
                    {
                        case "h":
                            game.PrintRules();
                            break;
                        case "s":
                            _gameStart = true;
                            break;
                        case "q":
                            Console.WriteLine("Program has ended succesfully!");
                            _gameStop = true;
                            break;
                    }
                } else
                {
                    Console.WriteLine("Invalid command!");
                }

            }

            game.Play();

            if (game.IsFinished && game.Turns.Count == 10)
            {
                Console.WriteLine($"\nSorry it's look like you didn't guess it, the correct number was {game.Code}");
            } else
            {
                Console.WriteLine($"\nYou were correct! The correct sequence of numbers is {game.Code}");
            }
            
        }
    }
}