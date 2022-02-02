using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler
{
    public class SampleWordListProvider : IWordListProvider
    {
        public List<string> GetWords()
        {
            return new List<string>
            {
                "cigar",
                "rebut",
                "sissy",
                "humph",
                "awake",
                "blush",
                "focal",
                "evade",
                "naval",
                "serve",
                "heath",
                "dwarf",
                "model",
                "karma",
                "stink",
                "grade",
                "quiet",
                "bench",
                "abate",
                "feign"
            }.Select(x => x.Trim().ToUpper()).ToList();
        }
    }
}
