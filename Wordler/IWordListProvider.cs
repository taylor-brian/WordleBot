using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordler
{
    public interface IWordListProvider
    {
        public List<string> GetWords();
    }
}
