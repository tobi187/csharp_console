using Combination.interfaces;
using Combination.Logic.Adding;
using Combination.Logic.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combination.Logic
{
    public class AddNumber
    {
        public AddFile file { get; set; }
        public string value { get; set; }

        public AddNumber(AddFile file, string value)
        {
            this.file = file;
            this.value = value;
        }
    }

    public class PosNumber
    {
        public PosFile file { get; set; }
        public int lineNum { get; set; }
        public int indexInLine { get; set; }
        public string[] value { get; set; }

        public PosNumber(string val, int lineN, int inLineInd, PosFile f)
        {
            file = f;
            lineNum = lineN;
            indexInLine = inLineInd;
            value = FillPosition(val);
        }

        public string[] FillPosition(string val)
        {
            var positions = new string[val.Length / 2];
            var pos = 0;
            
            for (var i = 0; i < val.Length / 2; i += 2)
            {
                positions[pos] = val.Substring(i, 2);
                pos++;
            }

            return positions;
        }
    }
}
