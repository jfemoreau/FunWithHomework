using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FunWithHomework.Models
{
    public class Range
    {
        [JsonInclude]
        public int Min { get; set; }

        [JsonInclude]
        public int Max { get; set; }

        public Range(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}
