using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler
{
    public class StatisticalWordSorter : IWordSorter
    {
        private WordListFilterer _wordListFilterer;
        private int _i = 0;

        public List<string> SortWords(List<string> remainingWords, List<WordResult> wordResults)
        {
            _wordListFilterer = new WordListFilterer();
            _i = 0;
            var wordStatistics = remainingWords.Select(x => new
            {
                Word = x,
                AverageRemainingWords = GetAverageRemainingWords(x, remainingWords, wordResults)
            }).OrderBy(x => x.AverageRemainingWords).ToList();
            
            File.WriteAllLines("TopChoices.txt", wordStatistics.Select(x => $"{x.Word}: {x.AverageRemainingWords}"));
            return wordStatistics.Select(x => x.Word).ToList();
        }

        private int GetAverageRemainingWords(string word, List<string> remainingWords, List<WordResult> wordResults)
        {
            var percentComplete = Convert.ToDecimal(_i++) / remainingWords.Count;
            /*Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"{percentComplete:P1}");*/

            int remainingWordsTotal = 0;

            foreach(var hypotheticalAnswer in remainingWords)
            {
                var wordResult = new WordChecker(hypotheticalAnswer).CheckWord(word);
                var newWordResults = new List<WordResult>(wordResults);
                newWordResults.Add(wordResult);
                remainingWordsTotal += _wordListFilterer.FilterList(remainingWords, newWordResults).Count;
            }
            
            return remainingWordsTotal / remainingWords.Count;
        }
    }
}
