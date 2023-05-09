using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    public static class Answer
    {
        public static readonly string CorrectGuess = "++++";
        public static readonly string CorrectNumberAndPlace = "+";
        public static readonly string CorrectNumberWrongPlace = "-";
        public static readonly string WrongNumber = " ";

        public const int ANSWER_LENGTH = 4;

        public static int[] GenerateRandomAnswer()
        {
            // An answer will consist of ANSWER_LENGTH digits 1 - 6
            var result = new int[ANSWER_LENGTH];
            var rand = new Random();
            for (int i = 0; i < ANSWER_LENGTH; i++)
            {
                result[i] = GetNumberNotInArray(result.AsEnumerable(), rand);
            }
            return result;
        }

        private static int GetNumberNotInArray(IEnumerable<int> resultList, Random rand)
        {
            var digit = rand.Next(1, 6);
            while (resultList.Contains(digit))
            {
                digit = new Random().Next(1, 6);
            }

            return digit;
        }
    }


}
