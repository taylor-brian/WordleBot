using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler
{
    public class WordChecker : IWordResultProvider
    {
        private string _answer;

        public WordChecker(string answer)
        {
            _answer = answer;
        }

        public WordResult CheckWord(string word)
        {
            var result = new WordResult();

            for(var i = 0; i < word.Length; i++)
            {
                result.Letters.Add(new WordResult.Letter
                {
                    Value = word[i],
                    Result = 
                        _answer[i] == word[i] ? WordResult.LetterResult.Green :
                        _answer.Contains(word[i]) ? WordResult.LetterResult.Yellow :
                        WordResult.LetterResult.Grey
                });
            }

            return result;
        }
    }
}
