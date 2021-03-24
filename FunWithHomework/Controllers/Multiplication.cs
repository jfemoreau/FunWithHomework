using System;

namespace FunWithHomework.Controllers
{
    public class Multiplication : MathOperation
    {
        public Multiplication()
        {
            var mathOperatioModel = new Models.MathOperationModel("Multiplication", "X", new Tuple<int, int>(0, 12), new Tuple<int, int>(0, 12));
            SetMathOperationModel(mathOperatioModel);
        }

        protected override int Operation(int firstNumber, int secondNumber)
        {
            return firstNumber * secondNumber;
        }
    }
}
