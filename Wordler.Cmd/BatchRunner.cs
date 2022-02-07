using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler.Cmd
{
    public class BatchRunner
    {
        string _firstWord = "GLORY";

        public void BatchRun()
        {
            IWordListProvider wordListProvider = new FileWordListProvider(false);
            IWordSorter wordSorter = new FirstMatchWordSorter();
            var wordsList = wordListProvider.GetWords();
            var firstMatchGuesses = 0;
            var statisticalHardModeGuesses = 0;
            var statisticalNormalModeGuesses = 0;
            var answers = GetAnswers();

            foreach (var answer in answers)
            {
                IWordResultProvider wordChecker = new WordChecker(answer);
                firstMatchGuesses += Run(wordsList, new FirstMatchWordSorter(), wordChecker, true);
                statisticalHardModeGuesses += Run(wordsList, new StatisticalWordSorter(), wordChecker, true);
                //statisticalNormalModeGuesses += Run(wordsList, new StatisticalWordSorter(), wordChecker, false);
            }

            Console.WriteLine($"First Candidate Average (Hard): {(Convert.ToDecimal(firstMatchGuesses) / answers.Count):N1}");
            Console.WriteLine($"Statistics-based Average (Hard): {(Convert.ToDecimal(statisticalHardModeGuesses) / answers.Count):N1}");
            //Console.WriteLine($"Statistics-based Average (Normal): {(Convert.ToDecimal(statisticalNormalModeGuesses) / answers.Count):N1}");
        }

        private int Run(List<string> wordsList, IWordSorter wordSorter, IWordResultProvider wordChecker, bool hardMode)
        {
            var potentialWords = new List<string>(wordsList);
            var selectableWords = new List<string>(wordsList);
            var wordResults = new List<WordResult>();
            var wordListFilterer = new WordListFilterer();
            var attempt = 1;

            while (true)
            {
                var word =
                    attempt == 1 && _firstWord != null && wordSorter is StatisticalWordSorter ? _firstWord :
                    potentialWords.Count == 1 ? potentialWords[0] :
                    wordSorter.SortWords(potentialWords, selectableWords, wordResults).First();
                var result = wordChecker.CheckWord(word);

                if (result.IsCorrect)
                {
                    Console.WriteLine($"{wordSorter.GetType().Name} ({(hardMode ? "Hard" : "Normal")}) found {word} in {attempt} guesses.");
                    return attempt;
                }

                wordResults.Add(result);
                potentialWords.Remove(word);
                potentialWords = wordListFilterer.FilterList(potentialWords, wordResults);
                
                if (hardMode)
                {
                    selectableWords = potentialWords;
                }

                attempt++;
            }
        }

        private List<string> GetAnswers()
        {
            return new List<string>
            {
"MOUNT",
"PERKY",
"COULD",
"WRUNG",
"LIGHT",
"THOSE",
"MOIST",
"SHARD",
"PLEAT"

            };
        }
    }
}
