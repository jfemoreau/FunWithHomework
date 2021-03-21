using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunWithHomework.Models
{
    public class Addition : MathOperation
    {
        public Addition()
        {
            Title = "Addition";
            Symbol = "+";
            FirstNumberRange = new Tuple<int, int>(0, 100);
            SecondNumberRange = new Tuple<int, int>(0, 100);
        }

        protected override int Operation(int firstNumber, int secondNumber)
        {
            return firstNumber + secondNumber;
        }
    }
}
