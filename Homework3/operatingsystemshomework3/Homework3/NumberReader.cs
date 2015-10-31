using System;
using System.Collections.Generic;
using System.IO;

namespace Homework3
{
    internal class NumberReader : IDisposable
    {
        private readonly StreamReader _reader;
        private readonly long[] _arr;

      /*Had some probles trying to make it read in with bytes instead of a stream
       * File.ReadAllBytes() gave trouble because it read in differently from expected
       * So in the end just ended up sticking with the StreamReader way of reading in the file
       */

        public NumberReader(FileSystemInfo file)
        {
            //dynamically allocate array and create streamReader
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
                //preload all of the numbers to not have to wait on I/O
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

