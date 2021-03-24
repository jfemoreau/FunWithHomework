using FunWithHomework.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunWithHomework.Controllers
{
    public abstract class MathOperation
    {
        private uint numberOfAttemps = 1;
        private Tuple<int, int> _currentNumberTuple;

        protected MathOperationModel MathOperationModel;
        protected List<Tuple<int, int>> NumberTuples { get; set; }

        public string Title { get => MathOperationModel?.Title; }
        public string Symbol { get => MathOperationModel?.Symbol; }
        public string VerificationMessage { get; protected set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string LastAnswer { get; protected set; } = string.Empty;
        public int FirstNumber { get; protected set; }
        public int SecondNumber { get; protected set; }
        public bool Success { get; protected set; }

        public void SetNextNumbers()
        {
            ResetAttemps();
            Answer = string.Empty;

            if (MathOperationModel != null)
            {
                if (NumberTuples == null || NumberTuples.Count == 0)
                {
                    FillNumberTupleList();
                }

                var random = new Random();
                var currentNumberTupleIndex = random.Next(0, NumberTuples.Count - 1);

                _currentNumberTuple = NumberTuples.ElementAt(currentNumberTupleIndex);

                FirstNumber = _currentNumberTuple.Item1;
                SecondNumber = _currentNumberTuple.Item2;
            }
        }

        protected virtual void FillNumberTupleList()
        {
            NumberTuples = new List<Tuple<int, int>>(MathOperationModel.FirstNumberRange.Item2 * MathOperationModel.SecondNumberRange.Item2);

            for (int firstNumberIndex = MathOperationModel.FirstNumberRange.Item1; firstNumberIndex <= MathOperationModel.FirstNumberRange.Item2; firstNumberIndex++)
            {
                var uperBound = MathOperationModel.AllowSecondNumberToBeGreater ? MathOperationModel.SecondNumberRange.Item2 : firstNumberIndex;

                for (int secondNumberIndex = MathOperationModel.SecondNumberRange.Item1; secondNumberIndex <= uperBound; secondNumberIndex++)
                {
                    NumberTuples.Add(new Tuple<int, int>(firstNumberIndex, secondNumberIndex));
                }
            }
        }

        private void ResetAttemps()
        {
            numberOfAttemps = 1;
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
                var rightAnswer = Operation(FirstNumber, SecondNumber);
                Success = rightAnswer == answerInInt;
                if (Success)
                {
                    VerificationMessage = "Bonne réponse!";
                    LastAnswer = $"{FirstNumber} {MathOperationModel.Symbol} {SecondNumber} = {answerInInt}";

                    NumberTuples.Remove(_currentNumberTuple);
                    SetNextNumbers();
                }
                else
                {
                    if (numberOfAttemps < MathOperationModel.NumberAttemps)
                    {
                        numberOfAttemps++;
                        VerificationMessage = "Mauvaise réponse.";
                    }
                    else
                    {
                        VerificationMessage = $"La bonne réponse est {FirstNumber} {MathOperationModel.Symbol} {SecondNumber} = {rightAnswer}.";
                        SetNextNumbers();
                    }

                    LastAnswer = string.Empty;
                }
            }
            else
            {
                VerificationMessage = "Entre un nombre.";
                LastAnswer = string.Empty;
            }
        }

        public void SetMathOperationModel(MathOperationModel mathOperationModel)
        {
            if (mathOperationModel == null)
            {
                throw new ArgumentNullException(nameof(mathOperationModel));
            }
            MathOperationModel = mathOperationModel;
        }
        protected abstract int Operation(int firstNumber, int secondNumber);
    }
}
