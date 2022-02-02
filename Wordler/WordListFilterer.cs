using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler
{
    public class WordListFilterer
    {
        public List<string> FilterList(List<string> wordsList, List<WordResult> wordResults)
        {
            var yellowLetters = wordResults.SelectMany(x =>
                x.Letters.Where(y => y.Result == WordResult.LetterResult.Yellow).Select(y => y.Value)).ToList();
            var greyLetters = wordResults.SelectMany(x => 
                x.Letters.Where(y => y.Result == WordResult.LetterResult.Grey).Select(y => y.Value)).ToList();
            var length = wordsList.First().Length;
            var greenLetters = new List<char?>();
            var filteredWordsList = new List<string>();

            for(var i = 0; i < length; i++)
            {
                greenLetters.Add(FindGreenLetter(wordResults, i));
            }

            foreach(var word in wordsList)
            {
                var failedGreens = false;

                if (word.Any(x => greyLetters.Contains(x)))
                {
                    continue;
                }

                for (var i = 0; i < length; i++)
                {
                    if (greenLetters[i].HasValue && word[i] != greenLetters[i])
                    {
                        failedGreens = true;
                        break;
                    }
                }

                if (failedGreens)
                {
                    continue;
                }

                if (!yellowLetters.All(x => word.Contains(x)))
                {
                    continue;
                }

                filteredWordsList.Add(word);
            }

            return filteredWordsList;
        }

        private char? FindGreenLetter(List<WordResult> wordResults, int index)
        {
            foreach(var word in wordResults)
            {
                if (word.Letters[index].Result == WordResult.LetterResult.Green)
                {
                    return word.Letters[index].Value;
                }
            }

            return null;
        }
    }
}
