using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FunWithHomework.Models
{
    public class Spelling
    {
        [JsonInclude]
        public string Title { get; set; }

        [JsonInclude]
        public string Language { get; set; }

        [JsonInclude]
        public List<string> Words { get; set; }

    }
}
