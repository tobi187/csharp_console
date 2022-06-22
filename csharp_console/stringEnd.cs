using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console
{
    internal class stringEnd
    {
        /*solution('abc', 'bc') // returns true
        solution('abc', 'd') // returns false*/

        public bool Solution(string str, string ending)
        {
            if (ending.Length > str.Length)
                return false;
            char[] ganzerString = str.ToCharArray();
            char[] ende  = ending.ToCharArray();
            Array.Reverse(ganzerString);    
            Array.Reverse(ende);        

            for (int i = 0; i < ende.Length; i++)
            {
                if (ganzerString[i] != ende[i])
                    return false;
            }
            return true;
        }
    }
}
