using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combination.interfaces
{
    public interface IResult
    {
        public List<IDataFile> files { get; set; }
        public IDataFile mainFile { get; set; }
        public List<string> results { get; set; }
        public int saveNum { get; set; }
        public int count => results.Count;
        public string basePath { get; set; }
        public string baseName { get; set; }

        public void Save();
        public void CleanUp();
    }
}
