using ContactAnalyser.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ContactAnalyser.Domain.Data
{
    public class FileProvider : IFileProvider
    {
        public TextReader GetTextReader(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Input file:" + path + " does not exist");

            return File.OpenText(path);
        }

        public TextWriter GetTextWriter(string path)
        {
            if (File.Exists(path))
                throw new FileLoadException("file already exists");

            return File.CreateText(path);
        }
    }
}
