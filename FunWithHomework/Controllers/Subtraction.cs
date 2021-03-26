using FunWithHomework.Models;
using System;
using System.Collections.Generic;

namespace FunWithHomework.Controllers
{
    public class Subtraction : MathOperation
    {
        public Subtraction(JsonStorageController jsonStorageController) : base(jsonStorageController)
        {
        }

        public override MathOperationModel GetDefaultModel()
        {
            return new MathOperationModel("Soustraction", "-", new Models.Range(0, 100), new Models.Range(0, 100), 3, true, false);
        }

        protected override int Operation(int firstNumber, int secondNumber)
        {
            return firstNumber - secondNumber;
        }
    }
}
