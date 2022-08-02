using Combination.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combination.Logic.Position
{
    public class PosFile
    {
        public List<PosNumber> numbers { get; set; }
        public string[] posNumber { get; set; }
        public int fileNum { get; set; }

        public PosFile(int fileNumber)
        {
            fileNum = fileNumber; 
        }

        public void FilterLines(string filterVal)
        {
            numbers =
                numbers
                .Where(x => !x.value.Contains(filterVal))
                .ToList();
        }
        
    }

    public class PosMainFile : PosFile
    {
        PosResult _result;
        public PosMainFile(int fileNum, PosResult result) : base(fileNum)
        {
            _result = result;
        }

        public void DoPosition()
        {

        }
    }
}
