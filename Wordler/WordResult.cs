using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler
{
    public class WordResult
    {
        public List<Letter> Letters { get; set; } = new List<Letter>();

        public bool IsCorrect
        {
            get
            {
                return Letters.All(x => x.Result == LetterResult.Green);
            }
        }

        public enum LetterResult
        {
            Grey,
            Yellow,
            Green
        }

        public class Letter
        {
            public char Value { get; set; }
            public LetterResult Result { get; set; }
        }
    }
}
