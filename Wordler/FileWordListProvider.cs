using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler
{
    public class FileWordListProvider : IWordListProvider
    {
        private bool _fullList = false;

        public FileWordListProvider(bool fullList)
        {
            _fullList = fullList;
        }

        public List<string> GetWords()
        {
            var words = File.ReadAllLines("wordle-simple.txt").ToList();

            if (_fullList)
            {
                words.AddRange(File.ReadAllLines("wordle-expanded.txt"));
            }

            return words.Select(x => x.Trim().ToUpper()).ToList();
        }
    }
}
