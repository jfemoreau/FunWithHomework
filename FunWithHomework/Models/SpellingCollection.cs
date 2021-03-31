using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunWithHomework.Models
{
    public class SpellingCollection : ConcurrentDictionary<int, Spelling>
    {
    }
}
