using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler.Cmd
{
    public class BatchRunner
    {
        string _firstWord = "RAISE";

        public void BatchRun()
        {
            IWordListProvider wordListProvider = new FileWordListProvider(false);
            IWordSorter wordSorter = new FirstMatchWordSorter();
            var wordsList = wordListProvider.GetWords();
            var firstMatchGuesses = 0;
            var statisticalGuesses = 0;
            var answers = GetAnswers();

            foreach (var answer in answers)
            {
                IWordResultProvider wordChecker = new WordChecker(answer);
                firstMatchGuesses += Run(new List<string>(wordsList), new FirstMatchWordSorter(), wordChecker);
                statisticalGuesses += Run(new List<string>(wordsList), new StatisticalWordSorter(), wordChecker);
            }

            Console.WriteLine($"First Candidate Average: {(Convert.ToDecimal(firstMatchGuesses) / answers.Count):N1}");
            Console.WriteLine($"Statistics-based Average: {(Convert.ToDecimal(statisticalGuesses) / answers.Count):N1}");
        }

        public int Run(List<string> wordsList, IWordSorter wordSorter, IWordResultProvider wordChecker)
        {
            var wordResults = new List<WordResult>();
            var wordListFilterer = new WordListFilterer();
            var attempt = 1;

            while (true)
            {
                var word =
                    attempt == 1 && _firstWord != null && wordSorter is StatisticalWordSorter ? _firstWord :
                    wordSorter.SortWords(wordsList, wordResults).First();
                var result = wordChecker.CheckWord(word);

                if (result.IsCorrect)
                {
                    Console.WriteLine($"{wordSorter.GetType().Name} found {word} in {attempt} guesses.");
                    return attempt;
                }

                wordResults.Add(result);
                wordsList.Remove(word);
                wordsList = wordListFilterer.FilterList(wordsList, wordResults);
                attempt++;
            }
        }

        private List<string> GetAnswers()
        {
            return new List<string>
            {
            "EARLY",
            "CHURN",
            "WEEDY",
            "STUMP",
            "LEASE",
            "WITTY",
            "WIMPY",
            "SPOOF",
            "SANER",
            "BLEND",
            "SALSA",
            "THICK",
            "WARTY",
            "MANIC",
            "BLARE",
            "SQUIB",
            "SPOON",
            "PROBE",
            "CREPE",
            "KNACK",
            "FORCE",
            "DEBUT",
            "ORDER",
            "HASTE",
            "TEETH",
            "AGENT",
            "WIDEN",
            "ICILY",
            "SLICE",
            "INGOT",
            "CLASH",
            "JUROR",
            "BLOOD",
            "ABODE",
            "THROW",
            "UNITY",
            "PIVOT",
            "SLEPT",
            "TROOP",
            "SPARE",
            "SEWER",
            "PARSE",
            "MORPH",
            "CACTI",
            "TACKY",
            "SPOOL",
            "DEMON",
            "MOODY",
            "ANNEX",
            "BEGIN",
            "FUZZY",
            "PATCH",
            "WATER",
            "LUMPY",
            "ADMIN",
            "OMEGA",
            "LIMIT",
            "TABBY",
            "MACHO",
            "AISLE",
            "SKIFF",
            "BASIS",
            "PLANK",
            "VERGE",
            "BOTCH",
            "CRAWL",
            "LOUSY",
            "SLAIN",
            "CUBIC",
            "RAISE",
            "WRACK",
            "GUIDE",
            "FOIST",
            "CAMEO",
            "UNDER",
            "ACTOR",
            "REVUE",
            "FRAUD",
            "HARPY",
            "SCOOP",
            "CLIMB",
            "REFER",
            "OLDEN",
            "CLERK",
            "DEBAR",
            "TALLY",
            "ETHIC",
            "CAIRN",
            "TULLE",
            "GHOUL",
            "HILLY",
            "CRUDE",
            "APART",
            "SCALE",
            "OLDER",
            "PLAIN",
            "SPERM",
            "BRINY",
            "ABBOT",
            "RERUN"
            };
        }
    }
}
