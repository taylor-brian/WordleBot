using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler
{
    public interface IWordResultProvider
    {
        public WordResult CheckWord(string word);
    }
}
