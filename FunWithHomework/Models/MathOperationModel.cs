using System;

namespace FunWithHomework.Models
{
    public class MathOperationModel
    {
        public string Title { get; protected set; }
        public string Symbol { get; protected set; }
        public Tuple<int, int> FirstNumberRange { get; set; }
        public Tuple<int, int> SecondNumberRange { get; set; }

        public MathOperationModel(string title, string symbol, Tuple<int,int> firstNumberRange, Tuple<int, int> secondNumberRange)
        {
            Title = title;
            Symbol = symbol;
            FirstNumberRange = firstNumberRange;
            SecondNumberRange = secondNumberRange;
        }
    }
}
