using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler
{
    public class FirstMatchWordSorter : IWordSorter
    {
        public List<string> SortWords(List<string> remainingWords, List<WordResult> wordResults)
        {
            return remainingWords;
        }
    }
}
