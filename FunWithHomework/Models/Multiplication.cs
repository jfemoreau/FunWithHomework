using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunWithHomework.Models
{
    public class Multiplication : MathOperation
    {
        public Multiplication()
        {
            Title = "Multiplication";
            Symbol = "X";
            FirstNumberRange = new Tuple<int, int>(0, 12);
            SecondNumberRange = new Tuple<int, int>(0, 12);
        }

        protected override int Operation(int firstNumber, int secondNumber)
        {
            return firstNumber * secondNumber;
        }
    }
}
