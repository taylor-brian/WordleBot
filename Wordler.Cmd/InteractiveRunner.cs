using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler.Cmd
{
    internal class InteractiveRunner
    {
        public void Run()
        {
            //IWordListProvider wordListProvider = new SampleWordListProvider();
            IWordListProvider wordListProvider = new FileWordListProvider(false);
            //IWordSorter wordSorter = new FirstMatchWordSorter();
            IWordSorter wordSorter = new StatisticalWordSorter();
            var wordsList = wordListProvider.GetWords();
            var answer = GetAnswer(wordsList);
            IWordResultProvider wordChecker = new WordChecker(answer);
            var wordResults = new List<WordResult>();
            var wordListFilterer = new WordListFilterer();
            string firstWord = "RAISE";

            for (var attempt = 1; attempt <= 6; attempt++)
            {
                var word =
                    attempt == 1 && firstWord != null && wordSorter is StatisticalWordSorter ? firstWord :
                    wordSorter.SortWords(wordsList, wordsList, wordResults).First();
                Console.WriteLine($"\r\n{wordsList.Count} word(s) left...\r\n");
                Console.WriteLine($"Attempt {attempt}: {word}");
                var result = wordChecker.CheckWord(word);
                wordResults.Add(result);

                if (result.IsCorrect)
                {
                    var formattedResult = new ResultsFormatter().GetFormattedResults(wordResults, true);

                    Console.WriteLine($"Found it!");
                    Console.Write(formattedResult);
                    return;
                }

                wordsList = wordListFilterer.FilterList(wordsList, wordResults);

                Console.WriteLine($"Nope. Press Enter to continue");
                Console.ReadLine();
            }

            Console.WriteLine("Ok, I give up!");
        }

        public string GetAnswer(List<string> wordsList)
        {
            Console.Write("\r\nEnter a 5 letter word: ");
            var answer = Console.ReadLine()?.ToUpper() ?? "";

            if (wordsList.Contains(answer))
            {
                return answer;
            }

            Console.WriteLine("That word is not in the list.\r\n");

            return GetAnswer(wordsList);
        }
    }
}
