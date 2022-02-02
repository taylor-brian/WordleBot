using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler.Cmd
{
    public class ResultsFormatter
    {
        public string GetFormattedResults(List<WordResult> results, bool solved)
        {
            var builder = new StringBuilder($@"--WordleBot--
Wordle {(solved ? results.Count : "X")}/6");
            foreach(var result in results)
            {
                builder.Append("\r\n" + GetFormattedResult(result));
            }
            return builder.ToString();
        }

        private string GetFormattedResult(WordResult result)
        {
            var builder = new StringBuilder();
            var letterMap = new Dictionary<WordResult.LetterResult, string>
            {
                { WordResult.LetterResult.Green, ":large_green_square:" },
                { WordResult.LetterResult.Yellow, ":large_yellow_square:" },
                { WordResult.LetterResult.Grey, ":black_large_square:" },
            };
            foreach(var letter in result.Letters)
            {
                builder.Append(letterMap[letter.Result]);
            }
            return builder.ToString();
        }
    }
}
