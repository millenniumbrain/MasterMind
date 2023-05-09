using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    public class Game
    {
        private static readonly string _startText = "Enter S to start a new game!";
        private static readonly string _helpText = "Enter H to read how to play the game.";
        private static readonly string _quitText = "Enter Q to quit the game.";
        public const int NUM_OF_CHANCES = 10;
        public List<Turn> Turns = new List<Turn>();
        public readonly string Code;

        public bool IsFinished { get; set; }

        public Game(string code)
        {
            Code = code;
        }

        public void PrintRules()
        {
            Console.WriteLine("\nI have generated a random code:");
            Console.WriteLine(" - Each digit falls between 1 and 6, inclusive.");
            Console.WriteLine($" - There are only {Answer.ANSWER_LENGTH} digits.");
            Console.WriteLine("You have up to 10 tries to guess my secret code.");
            Console.WriteLine($"After each guess, I will give you a {Answer.ANSWER_LENGTH}-digit response:");
            Console.WriteLine(" - A '+' character means your guess was correct in that position.");
            Console.WriteLine(" - A '-' character means that digit is in the code, but not in that position.");
            Console.WriteLine(" - A ' ' means that digit is not in the code at all.");
        }

        public void PrintCommands()
        {
            Console.WriteLine(_startText);
            Console.WriteLine(_helpText);
            Console.WriteLine(_quitText);
        }

        public void Play()
        {
            while (!IsFinished)
            {
                if (Turns.Count > 9)
                {
                    IsFinished = true;
                }
                else
                {
                    var turn = new Turn(Turns.Count + 1, Code);
                    Console.Write($"{Environment.NewLine}Guess #{turn.Number}: ");

                    var guess = Console.ReadLine()?.Replace("\0", string.Empty);

                    bool isValidGuess = turn.TryParseGuess(guess, out int[] result);
                    if (isValidGuess)
                    {
                        var response = turn.CheckGuess(guess);
                        Console.WriteLine(response);
                        if (!string.IsNullOrEmpty(response) && response == Answer.CorrectGuess)
                        {
                            IsFinished = true;
                        }
                    }
                    else
                    {
                        HandleInvalidGuess();
                    }
                    Turns.Add(turn);
                }

            }
        }

        private void HandleInvalidGuess()
        {
            Console.WriteLine("Not a valid guess, that will cost you a turn.");
            Console.WriteLine("Do you want to read the rules? (Y) to read, or any key to guess again.");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                PrintRules();
                Console.WriteLine("Press any key to guess again");
                Console.ReadKey();
            }
        }
    }
}
