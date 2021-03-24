using System;

namespace FunWithHomework.Models
{
    public class MathOperationModel
    {
        public string Title { get; protected set; }
        public string Symbol { get; protected set; }
        public Tuple<int, int> FirstNumberRange { get; set; }
        public Tuple<int, int> SecondNumberRange { get; set; }
        public uint NumberAttemps { get; set; } = 3;
        public bool AllowSecondNumberToBeGreater { get; set; }

        public MathOperationModel(string title, string symbol, Tuple<int,int> firstNumberRange, Tuple<int, int> secondNumberRange, bool allowSecondNumberTobeGreater = true)
        {
            Title = title;
            Symbol = symbol;
            FirstNumberRange = firstNumberRange;
            SecondNumberRange = secondNumberRange;
            AllowSecondNumberToBeGreater = allowSecondNumberTobeGreater;
        }
    }
}
