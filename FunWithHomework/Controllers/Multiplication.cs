using FunWithHomework.Models;
namespace FunWithHomework.Controllers
{
    public class Multiplication : MathOperation
    {
        public Multiplication(JsonStorageController jsonStorageController) : base(jsonStorageController)
        {
        }

        public override MathOperationModel GetDefaultModel()
        {
            return new MathOperationModel("Multiplication", "X", new Range(0, 12), new Range(0, 12), 3, true, true);
        }

        protected override int Operation(int firstNumber, int secondNumber)
        {
            return firstNumber * secondNumber;
        }
    }
}
