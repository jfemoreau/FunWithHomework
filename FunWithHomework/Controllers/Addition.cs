using FunWithHomework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunWithHomework.Controllers
{
    public class Addition : MathOperation
    {
        public Addition()
        {
            var mathOperatioModel = new MathOperationModel("Addition", "+", new Tuple<int, int>(0, 100), new Tuple<int, int>(0, 100));
            SetMathOperationModel(mathOperatioModel);
        }

        protected override int Operation(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }
    }
}
