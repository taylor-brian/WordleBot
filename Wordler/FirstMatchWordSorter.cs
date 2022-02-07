using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler
{
    public class FirstMatchWordSorter : IWordSorter
    {
        public List<string> SortWords(List<string> potentialWords, List<string> selectableWords, List<WordResult> wordResults)
        {
            Random rng = new Random(); 
            int n = potentialWords.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = potentialWords[k];
                potentialWords[k] = potentialWords[n];
                potentialWords[n] = value;
            }
            return potentialWords;
        }
    }
}
