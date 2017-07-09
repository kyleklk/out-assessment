using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAnalyser.Contracts
{
    public interface IFileProvider
    {
        TextWriter GetTextWriter(string path);

        TextReader GetTextReader(string path);
    }
}
