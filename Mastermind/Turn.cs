using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mastermind
{
    public class Turn
    {
        private readonly string _code;
        public string? Response;
        public int Number;

        public Turn(int number, string code)
        {
            Number = number;
            _code = code;
        }

        public string CheckGuess(string guess)
        {
            var builder = new StringBuilder(guess.Length);

            for (var i = 0; i < guess.Length; i++)
            {
                var response = Answer.WrongNumber;

                var digit = guess[i];

                // check code and if the digit is there add the hint characters otherwise return nothing for the wrong number
                if (_code.Contains(digit))
                {
                    response = _code[i] == digit ? Answer.CorrectNumberAndPlace : Answer.CorrectNumberWrongPlace;
                }

                builder.Append(response);
            }

            Response = builder.ToString();

            return builder.ToString();
        }

        public bool TryParseGuess(string? guess, out int[] result)
        {
            result = new int[Answer.ANSWER_LENGTH];
            if (string.IsNullOrEmpty(guess) || guess.Length != Answer.ANSWER_LENGTH) return false;

            bool isValid = false;

            string pattern = "[1-6]{" + Answer.ANSWER_LENGTH + "}";
            var regEx = new Regex(pattern);
            if (regEx.IsMatch(guess))
            {
                isValid = true;
                for (int i = 0; i < Answer.ANSWER_LENGTH; i++)
                {
                    result[i] = (int)char.GetNumericValue(guess[i]);
                }
            }
            return isValid;
        }
    }
}
