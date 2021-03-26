using FunWithHomework.Models;
using System;

namespace FunWithHomework.Controllers
{
    public class Addition : MathOperation
    {
        public Addition(JsonStorageController jsonStorageController) : base(jsonStorageController)
        {            
        }

        public override MathOperationModel GetDefaultModel()
        {
            return new MathOperationModel("Addition", "+", new Models.Range(0, 100), new Models.Range(0, 100), 3, true, true);
        }

        protected override int Operation(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }
    }
}
