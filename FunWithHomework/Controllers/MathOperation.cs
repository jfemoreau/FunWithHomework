using FunWithHomework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunWithHomework.Controllers
{
    public abstract class MathOperation
    {
        protected MathOperationModel MathOperationModel;
        public string Title { get => MathOperationModel?.Title; }
        public string Symbol { get => MathOperationModel?.Symbol; }
        public string VerificationMessage { get; protected set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string LastAnswer { get; protected set; } = string.Empty;
        public int FirstNumber { get; protected set; }
        public int SecondNumber { get; protected set; }
        public bool Success { get; protected set; }

        public void SetNumbers()
        {
            if (MathOperationModel != null)
            {
                FirstNumber = GetNumber(MathOperationModel.FirstNumberRange);
                SecondNumber = GetNumber(MathOperationModel.SecondNumberRange);
            }
        }

        public int GetNumber(Tuple<int, int> range)
        {
            var random = new Random();
            return random.Next(range.Item1, range.Item2);
        }

        public void Verify()
        {
            if (MathOperationModel == null)
            {
                throw new InvalidOperationException("Math operation model is not set.");
            }

                Success = false;
            VerificationMessage = string.Empty;
            var tmpSuccess = int.TryParse(Answer.Trim(), out var answerInInt);

            if (tmpSuccess)
            {
                Success = Operation(FirstNumber, SecondNumber) == answerInInt;
                if (Success)
                {
                    VerificationMessage = "Bonne réponse!";
                    LastAnswer = $"{FirstNumber} {MathOperationModel.Symbol} {SecondNumber} = {answerInInt}";


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

        public void SetMathOperationModel(MathOperationModel mathOperationModel)
        {
            if(mathOperationModel == null)
            {
                throw new ArgumentNullException(nameof(mathOperationModel));
            }
            MathOperationModel = mathOperationModel;
        }
        protected abstract int Operation(int firstNumber, int secondNumber);
    }
}
