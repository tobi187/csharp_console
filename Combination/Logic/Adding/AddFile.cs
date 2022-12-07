using Combination.interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combination.Logic.Adding
{
    public class AddFile
    {
        public List<AddNumber> numbers { get; set; }
        public string[] posNumber { get; set; }
        public int fileNum { get; set; }

        public AddFile(int fileNumber)
        {
            fileNum = fileNumber;
        }

        public void FilterLines(string filterValue)
        {
            numbers =
                numbers
                .Where(x => !x.value.Contains(filterValue))
                .ToList();
        }
    }

    public class AddMainFile : AddFile
    {
        AddResult _result;
        public AddMainFile(int fileNum, AddResult result) : base(fileNum)
        {
            _result = result;
        }

        public void DoAdding(AddFile secondFile)
        {
            var sw = new Stopwatch();
            sw.Start();
            foreach (var line in numbers)
            {
                foreach (var line2 in secondFile.numbers)
                {
                    _result.results.Add(Add(line.value, line2.value));
                    if (_result.count == 200000) // safe after 200 entrys
                        _result.Save();
                }
            }

            sw.Stop();
            Console.WriteLine($"File {secondFile.fileNum} finished in {sw.ElapsedMilliseconds / 1000} sec");
        }

        public string Add(string f, string s)
        {
            int len = f.Length;
            var res = (int.Parse(f) + int.Parse(s)).ToString();
            return res.PadLeft(len);
        }
    }
}
