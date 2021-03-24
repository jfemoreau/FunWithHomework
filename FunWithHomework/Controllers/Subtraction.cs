using FunWithHomework.Models;
using System;
using System.Collections.Generic;

namespace FunWithHomework.Controllers
{
    public class Subtraction : MathOperation
    {
        public Subtraction()
        {
            var mathOperatioModel = new MathOperationModel("Soustraction", "-", new Tuple<int, int>(0, 100), new Tuple<int, int>(0, 100), false);
            SetMathOperationModel(mathOperatioModel);
        }

        protected override int Operation(int firstNumber, int secondNumber)
        {
            return firstNumber - secondNumber;
        }
    }
}
