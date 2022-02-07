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

        public List<string> SortWords(List<string> potentialWords, List<string> selectableWords, List<WordResult> wordResults)
        {
            _wordListFilterer = new WordListFilterer();
            var wordStatistics = selectableWords.Select(x => new
            {
                Word = x,
                AverageRemainingWords = GetAverageRemainingWords(x, potentialWords, wordResults)
            }).OrderBy(x => x.AverageRemainingWords).ThenByDescending(x => potentialWords.Contains(x.Word)).ToList();
            
            File.WriteAllLines("c:\\temp\\TopChoices.txt", wordStatistics.Select(x => $"{x.Word}: {x.AverageRemainingWords}"));
            return wordStatistics.Select(x => x.Word).ToList();
        }

        private decimal GetAverageRemainingWords(string word, List<string> potentialWords, List<WordResult> wordResults)
        {
            int remainingWordsTotal = 0;

            foreach(var potentialWord in potentialWords)
            {
                var wordResult = new WordChecker(potentialWord).CheckWord(word);
                var newWordResults = new List<WordResult>(wordResults);
                newWordResults.Add(wordResult);
                remainingWordsTotal += _wordListFilterer.FilterList(potentialWords, newWordResults).Count;
            }
            
            return Convert.ToDecimal(remainingWordsTotal) / potentialWords.Count;
        }
    }
}
