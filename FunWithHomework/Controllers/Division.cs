using FunWithHomework.Models;
using System;
using System.Collections.Generic;

namespace FunWithHomework.Controllers
{
    public class Division : MathOperation
    {
        public Division()
        {
            var mathOperatioModel = new MathOperationModel("Division", "÷", new Tuple<int, int>(1, 12), new Tuple<int, int>(1, 12), false);
            SetMathOperationModel(mathOperatioModel);
        }

        protected override int Operation(int firstNumber, int secondNumber)
        {
            return firstNumber / secondNumber;
        }

        protected override void FillNumberTupleList()
        {
            NumberTuples = new List<Tuple<int, int>>(MathOperationModel.FirstNumberRange.Item2 * MathOperationModel.SecondNumberRange.Item2);

            for (int firstNumberIndex = MathOperationModel.FirstNumberRange.Item1; firstNumberIndex <= MathOperationModel.FirstNumberRange.Item2; firstNumberIndex++)
            {
                for (int secondNumberIndex = MathOperationModel.SecondNumberRange.Item1; secondNumberIndex <= firstNumberIndex; secondNumberIndex++)
                {
                    if (firstNumberIndex % secondNumberIndex == 0)
                    {
                        NumberTuples.Add(new Tuple<int, int>(firstNumberIndex, secondNumberIndex));
                    }
                }
            }
        }
    }
}
