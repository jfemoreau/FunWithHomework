using FunWithHomework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunWithHomework.Controllers
{
    public class AdvancedDivision : Division
    {
        public string ModuloAnswer { get; set; }
        public AdvancedDivision(JsonStorageController jsonStorageController) : base(jsonStorageController)
        {
        }

        public override async Task Verify()
        {
            AssertMathOperationModelNotNull();

            Success = false;
            VerificationMessage = string.Empty;

            if (string.IsNullOrEmpty(Answer))
                Answer = "0";

            if (string.IsNullOrEmpty(ModuloAnswer))
                ModuloAnswer = "0";

            var tmpSuccess = int.TryParse(Answer.Trim(), out var answerInInt);
            var tmpModuloSuccess = int.TryParse(ModuloAnswer.Trim(), out var moduloAnswerInInt);

            if (tmpSuccess)
            {
                var rightAnswer = Operation(FirstNumber, SecondNumber);
                var rightModuloAnswer = ModuloOperation(FirstNumber, SecondNumber);

                Success = rightAnswer == answerInInt && rightModuloAnswer == moduloAnswerInInt;
                if (Success)
                {
                    VerificationMessage = GoodAnswerMessage;
                    LastAnswer = $"{FirstNumber} {MathOperationModel.Symbol} {SecondNumber} = {answerInInt} Reste {moduloAnswerInInt}";

                    NumberTuples?.Remove(CurrentNumberTuple);
                    await SetNextNumbersAsync();
                }
                else
                {
                    if (NumberOfAttemps < MathOperationModel.NumberAttemps)
                    {
                        NumberOfAttemps++;
                        VerificationMessage = BadAnswerMessage;
                    }
                    else
                    {
                        VerificationMessage = $"{GoodAnswerIsMessage} {FirstNumber} {MathOperationModel.Symbol} {SecondNumber} = {rightAnswer} Reste {rightModuloAnswer}.";
                        await SetNextNumbersAsync();
                    }

                    LastAnswer = string.Empty;
                }
            }
            else
            {
                VerificationMessage = EnterANumberMessage;
                LastAnswer = string.Empty;
            }
        }

        protected override void FillNumberTupleList()
        {
            VerifyAndCorrectRange();

            var numberOfEntries = Math.Abs(MathOperationModel.FirstNumberRange.Max * MathOperationModel.SecondNumberRange.Max);

            NumberTuples = new List<Tuple<int, int>>(numberOfEntries > 1000 ? 1000 : numberOfEntries);

            for (int firstNumberIndex = MathOperationModel.FirstNumberRange.Min; firstNumberIndex <= MathOperationModel.FirstNumberRange.Max; firstNumberIndex++)
            {
                for (int secondNumberIndex = MathOperationModel.SecondNumberRange.Min; secondNumberIndex <= firstNumberIndex; secondNumberIndex++)
                {
                    if (secondNumberIndex != 0)
                    {
                        NumberTuples.Add(new Tuple<int, int>(firstNumberIndex, secondNumberIndex));
                    }
                }
            }
        }

        protected int ModuloOperation(int firstNumber, int secondNumber)
        {
            if (secondNumber == 0)
                return 0;

            return firstNumber % secondNumber;
        }

        protected override void ResetAnswer()
        {
            base.ResetAnswer();
            ModuloAnswer = string.Empty;
        }

        public override MathOperationModel GetDefaultModel()
        {
            return new MathOperationModel("Division (Avancée)", "÷", new Models.Range(1, 100), new Models.Range(1, 100), 3, false, false);
        }
    }
}
