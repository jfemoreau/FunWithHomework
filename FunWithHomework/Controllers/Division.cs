using FunWithHomework.Models;
using System;
using System.Collections.Generic;

namespace FunWithHomework.Controllers
{
    public class Division : MathOperation
    {
        public Division(JsonStorageController jsonStorageController) : base(jsonStorageController)
        {
        }

        protected override int Operation(int firstNumber, int secondNumber)
        {
            if (secondNumber == 0)
                return 0;

            return firstNumber / secondNumber;
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
                    if (secondNumberIndex != 0 && firstNumberIndex % secondNumberIndex == 0)
                    {
                        NumberTuples.Add(new Tuple<int, int>(firstNumberIndex, secondNumberIndex));
                    }
                }
            }
        }

        public override MathOperationModel GetDefaultModel()
        {
            return new MathOperationModel("Division", "÷", new Models.Range(1, 100), new Models.Range(1, 12), 3, false, false);
        }
    }
}
