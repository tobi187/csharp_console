//
using Combination;
using System.Diagnostics;

// @"C:\Users\fisch\Desktop\combsData" => path of testfiles
// combinationTest_nr => name of testFile (0-10) 

//var cF = new CreateFiles(@"C:\Users\fisch\Desktop\combsData", "combinationTest");
//cF.SaveFiles("0102030405");

//return;


var t = new Stopwatch();
t.Start();

var fP = Enumerable.Range(1, 5).Select(i => $@"C:\Users\fisch\Desktop\combsData\combinationTest_{i}.txt").ToList();
var mP = @"C:\Users\fisch\Desktop\combsData\combinationTest_0.txt";
var sP = @"C:\Users\fisch\Desktop\combsData\results";
var mN = "combRes";
var res = new Result(fP, mP, sP, mN);

t.Stop();

Console.WriteLine($"Initialization took {t.ElapsedMilliseconds / 1000.0} sec");

res.StartAdding();

public class Number
{
    public DataFile file { get; set; }
    public int lineNum { get; set; }
    public int indexInLine { get; set; }
    public string value { get; set; }
    public string[] posVals { get; set; }
    
    public void FillPosition()
    {
        posVals = new string[value.Length / 2];
        var pos = 0;
        for (var i = 0; i < posVals.Length / 2; i += 2)
        {
            posVals[pos] = value.Substring(i, 2);
            pos++;
        }
    }
}

public class DataFile
{
    public List<Number> numbers { get; set; }
    public string[] posNumber { get; set; } 
    public int fileNum { get; set; }
    public DataFile(int fileNum)
    {
        this.fileNum = fileNum;
    }

    public void FilterLines(string filterVal)
    {
        numbers = 
            numbers
            .Where(x => !x.value.Contains(filterVal))
            .ToList();
    }

    public void DoPosition(string seachVal)
    {
        numbers.ForEach(x => x.FillPosition());
    }
}

class MainFile : DataFile
{
    public Result _result; 
    public MainFile(int fileNum, Result result) : base(fileNum)
    {
        _result = result;
    }

    

    public void DoAdding(DataFile secondFile)
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

class Result
{
    public List<DataFile> files { get; set; }
    public MainFile mainFile { get; set; }
    public List<string> results { get; set; }
    public int saveNum { get; set; } = 0;
    public int count => results.Count;
    public string basePath { get; set; }
    public string baseName { get; set; }
    

    public Result(List<string> filePaths, string mainPath, string bPath, string bName)
    {
        files = filePaths
            .Select((x, i) => 
            CreateDataFiles(x, i + 1))
            .ToList();
        mainFile = (MainFile)CreateDataFiles(mainPath, 0, isMainFile: true);
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
    

    public DataFile CreateDataFiles(string filePath, int nr, bool isMainFile = false)
    {
        var file = File.ReadAllLines(filePath);
        var dFile = isMainFile 
            ? new MainFile(nr, this) 
            : new DataFile(nr);

        var currLine = 0;
        var nums = new List<Number>();

        foreach (var line in file)
        {
            var tabParts = line.Split("\t");
            for (var i = 0; i < tabParts.Length; i++)
            {
                nums.Add(new Number()
                {
                    file = dFile,
                    lineNum = currLine,
                    indexInLine = i,
                    value = tabParts[i]
                });
            }
            currLine++;
        }
        dFile.numbers = nums;
        return dFile;
    }
}