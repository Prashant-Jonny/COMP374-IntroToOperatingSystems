using System;
using System.Collections.Generic;
using System.IO;

namespace Homework3
{
    internal class NumberReader : IDisposable
    {
        private readonly StreamReader _reader;
        private long[] _arr;

        public NumberReader(FileSystemInfo file)
        {
             var lineCount = File.ReadAllLines(file.FullName).Length;
             _arr = new long[lineCount];

            _reader = new StreamReader(new BufferedStream(new
                FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan), 65536));
        }

        public long[] ReadIntegers()
        {
            string x;
            var i = 0;
            while ((x = _reader.ReadLine()) != null)
            {
                _arr[i] = long.Parse(x);
                i++;
            }
            return _arr;
        }

        //Garbage collector should take care of this
        //for an  array automatically
        public void Dispose() { }

    }
}

