using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combination.interfaces
{
    public interface IDataFile
    {
        public List<INumber> numbers { get; set; }
        public string[] posNumber { get; set; }
        public int fileNum { get; set; }

        public void FilterLines(string filterValue);
    }
}
