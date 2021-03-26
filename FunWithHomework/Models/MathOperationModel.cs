using System;
using System.Text.Json.Serialization;

namespace FunWithHomework.Models
{
    public class MathOperationModel : ICloneable
    {
        [JsonInclude]
        public string Title { get; protected set; }

        [JsonInclude]
        public string Symbol { get; protected set; }

        [JsonInclude]
        public Range FirstNumberRange { get; set; }

        [JsonInclude]
        public Range SecondNumberRange { get; set; }

        [JsonInclude]
        public uint NumberAttemps { get; set; }

        [JsonInclude]
        public bool AllowSecondNumberToBeGreater { get; set; }

        [JsonInclude]
        public bool ShowAllowSecondNumberToBeGreaterSetting { get; set; }

        [JsonConstructor]
        public MathOperationModel() { }

        public MathOperationModel(string title, string symbol, Range firstNumberRange, Range secondNumberRange, uint numberOfAttemps,
                                  bool allowSecondNumberToBeGreater, bool showAllowSecondNumberToBeGreaterSetting)
        {
            Title = title;
            Symbol = symbol;
            FirstNumberRange = firstNumberRange;
            SecondNumberRange = secondNumberRange;
            NumberAttemps = numberOfAttemps;
            AllowSecondNumberToBeGreater = allowSecondNumberToBeGreater;
            ShowAllowSecondNumberToBeGreaterSetting = showAllowSecondNumberToBeGreaterSetting;
        }

        public object Clone()
        {
            return new MathOperationModel(Title, Symbol, FirstNumberRange, SecondNumberRange, NumberAttemps, AllowSecondNumberToBeGreater, ShowAllowSecondNumberToBeGreaterSetting);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
