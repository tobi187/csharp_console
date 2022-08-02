using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combination
{
    public class CreateFiles
    {
        public int amountFiles { get; set; }
        public int amountPerRow { get; set; }
        public int amountLines { get; set; }
        public string basePath { get; set; }
        public string baseName { get; set; }
        public CreateFiles(string basePath, string baseName, int amountFiles = 11, int amountPerRow = 10, int amountLines = 5000)
        {
            this.amountFiles = amountFiles;
            this.amountPerRow = amountPerRow;
            this.amountLines = amountLines;
            this.basePath = basePath;
            this.baseName = baseName + "_";
        }

        public void SaveFiles(string number)
        {
            for (var i = 0; i < amountFiles; i++)
            {
                var p = Path.Join(basePath, baseName + i + ".txt");
                File.WriteAllLines(p, GenerateData(number));
            }
        }

        public string[] GenerateData(string number)
        {
            var data = new string[amountLines];
            for (var row = 0; row < amountLines; row++)
                data[row] = string.
                    Join("\t", Enumerable.Repeat(number, amountPerRow));

            return data;
        }
    }
}
