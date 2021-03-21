using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunWithHomework.Models
{
    public abstract class MathOperation
    {
        public string Title { get; protected set; }
        public string Symbol { get; protected set; }
        protected Tuple<int, int> FirstNumberRange { get; set; }
        protected Tuple<int, int> SecondNumberRange { get; set; }
        public string VerificationMessage { get; protected set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string LastAnswer { get; protected set; } = string.Empty;
        public int FirstNumber { get; protected set; }
        public int SecondNumber { get; protected set; }
        public bool Success { get; protected set; }

        public void SetNumbers()
        {
            FirstNumber = GetNumber(FirstNumberRange);
            SecondNumber = GetNumber(SecondNumberRange);
        }

        public int GetNumber(Tuple<int, int> range)
        {
            var random = new Random();
            return random.Next(range.Item1, range.Item2);
        }

        public void Verify()
        {
            Success = false;
            VerificationMessage = string.Empty;
            var tmpSuccess = int.TryParse(Answer.Trim(), out var answerInInt);

            if (tmpSuccess)
            {
                Success = Operation(FirstNumber, SecondNumber) == answerInInt;
                if (Success)
                {
                    VerificationMessage = "Bonne réponse!";
                    LastAnswer = $"{FirstNumber} {Symbol} {SecondNumber} = {answerInInt}";


                    SetNumbers();
                    Answer = string.Empty;
                }
                else
                {
                    VerificationMessage = "Mauvaise réponse.";
                    LastAnswer = string.Empty;
                }
            }
            else
            {
                VerificationMessage = "Entre un chiffre.";
                LastAnswer = string.Empty;
            }
        }

        protected abstract int Operation(int firstNumber, int secondNumber);
    }
}
