using Combination.interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combination.Logic.Position
{
    public class PosResult 
    {
        public List<PosFile> files { get; set; }
        public PosMainFile mainFile { get; set; }
        public List<string> results { get; set; }
        public int saveNum { get; set; } = 0;
        public int count => results.Count;
        public string basePath { get; set; }
        public string baseName { get; set; }


        public PosResult(List<string> filePaths, string mainPath, string bPath, string bName)
        {
            files = filePaths
                .Select((x, i) =>
                CreateDataFiles(x, i + 1))
                .ToList();
            mainFile = (PosMainFile)CreateDataFiles(mainPath, 0, isMainFile: true);
            results = new List<string>();
            saveNum = 0;
            basePath = bPath;
            baseName = bName;
        }

        public void StartAdding()
        {
            var timer = new Stopwatch();
            timer.Start();
            foreach (var file in files)
            {
                mainFile.DoAdding(file);
            }
            timer.Stop();
            Console.WriteLine($"Writing 10 files (2K lines, 10 per line) in {timer.ElapsedMilliseconds / 1000.0} sec");
        }

        public void Save()
        {
            var path = Path.Join(basePath, $"{baseName}({saveNum}).txt");
            File.WriteAllText(path, string.Join("\t", results));
            saveNum++;
            results.Clear();
        }

        public void Filter(string filter)
        {
            mainFile.FilterLines(filter);
            files.ForEach(x => x.FilterLines(filter));
        }

        public void CleanUp(bool delete = true)
        {

        }


        public PosFile CreateDataFiles(string filePath, int nr, bool isMainFile = false)
        {
            var file = File.ReadAllLines(filePath);
            var dFile = isMainFile
                ? new PosMainFile(nr, this)
                : new PosFile(nr);

            var currLine = 0;
            var nums = new List<PosNumber>();

            foreach (var line in file)
            {
                var tabParts = line.Split("\t");
                for (var i = 0; i < tabParts.Length; i++)
                {
                    nums.Add(
                        new PosNumber(
                            tabParts[i], 
                            currLine, 
                            i, 
                            dFile));
                }
                currLine++;
            }
            dFile.numbers = nums;
            return dFile;
        }
    }
}
