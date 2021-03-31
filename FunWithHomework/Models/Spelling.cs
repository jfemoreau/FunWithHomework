using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FunWithHomework.Models
{
    public class Spelling: ICloneable
    {
        [JsonInclude]
        public string Title { get; set; }

        [JsonInclude]
        public DateTime DateCreated { get; set; }

        [JsonInclude]
        public string Language { get; set; }

        [JsonInclude]
        public List<string> Words { get; set; }

        public string GetId()
        {
            return $"{Title}-{DateCreated}";
        }

        public override int GetHashCode()
        {
            return GetId().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Spelling);
        }

        public bool Equals(Spelling other)
        {
            return other != null && GetId() == other.GetId();
        }

        public object Clone()
        {
            return new Spelling
            {
                DateCreated = DateCreated,
                Language = Language,
                Title = Title,
                Words = Words
            };
        }
    }
}
