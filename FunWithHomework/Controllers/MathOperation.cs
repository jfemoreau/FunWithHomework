using FunWithHomework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunWithHomework.Controllers
{
    public abstract class MathOperation
    {
        private uint numberOfAttemps = 1;
        private Tuple<int, int> _currentNumberTuple;
        private JsonStorageController _jsonStorageController;

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

        public MathOperation(JsonStorageController jsonStorageController)
        {
            _jsonStorageController = jsonStorageController;
            var mathOperatioModel = GetDefaultModel();
            SetMathOperationModel(mathOperatioModel);
        }

        public async Task SetNextNumbersAsync()
        {
            ResetAttemps();
            Answer = string.Empty;

            if (MathOperationModel != null)
            {
                if (NumberTuples == null || NumberTuples.Count == 0)
                {
                    var model = await _jsonStorageController.Retrieve(MathOperationModel);
                    if(model != null)
                    {
                        MathOperationModel = model;
                    }
                    
                    await Task.Run(() => FillNumberTupleList());
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
            VerifyAndCorrectRange();

            var numberOfEntries = Math.Abs(MathOperationModel.FirstNumberRange.Max * MathOperationModel.SecondNumberRange.Max);

            NumberTuples = new List<Tuple<int, int>>(numberOfEntries > 1000 ? 1000 : numberOfEntries) ;

            for (int firstNumberIndex = MathOperationModel.FirstNumberRange.Min; firstNumberIndex <= MathOperationModel.FirstNumberRange.Max; firstNumberIndex++)
            {
                var uperBound = MathOperationModel.AllowSecondNumberToBeGreater ? MathOperationModel.SecondNumberRange.Max : firstNumberIndex;

                for (int secondNumberIndex = MathOperationModel.SecondNumberRange.Min; secondNumberIndex <= uperBound; secondNumberIndex++)
                {
                    NumberTuples.Add(new Tuple<int, int>(firstNumberIndex, secondNumberIndex));
                }
            }
        }

        protected void VerifyAndCorrectRange()
        {
            if (MathOperationModel.FirstNumberRange.Max <= MathOperationModel.FirstNumberRange.Min)
                MathOperationModel.FirstNumberRange.Max = MathOperationModel.FirstNumberRange.Min + 1;

            if (MathOperationModel.SecondNumberRange.Max <= MathOperationModel.SecondNumberRange.Min)
                MathOperationModel.SecondNumberRange.Max = MathOperationModel.SecondNumberRange.Min + 1;

            if (MathOperationModel.FirstNumberRange.Max - MathOperationModel.FirstNumberRange.Min >= 10000)
                MathOperationModel.FirstNumberRange.Max = MathOperationModel.FirstNumberRange.Min + 10000;

            if (MathOperationModel.SecondNumberRange.Max - MathOperationModel.SecondNumberRange.Min >= 10000)
                MathOperationModel.SecondNumberRange.Max = MathOperationModel.SecondNumberRange.Min + 10000;
        }

        private void ResetAttemps()
        {
            numberOfAttemps = 1;

        }

        public async Task Verify()
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

                    NumberTuples?.Remove(_currentNumberTuple);
                    await SetNextNumbersAsync();
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
                        await SetNextNumbersAsync();
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

        public MathOperationModel GetMathOperationModel()
        {
            if (MathOperationModel == null)
            {
                return null;
            }

            return MathOperationModel.Clone() as MathOperationModel;
        }

        public void ResetNumbers()
        {
            NumberTuples = null;
            Success = false;
            VerificationMessage = string.Empty;
            LastAnswer = string.Empty;
            Answer = string.Empty;
        }
        protected abstract int Operation(int firstNumber, int secondNumber);
        public abstract MathOperationModel GetDefaultModel();
    }
}
