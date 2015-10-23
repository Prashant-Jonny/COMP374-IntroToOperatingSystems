using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Homework3
{
    internal class NumberReader : IDisposable
    {
        private Byte[] arr;

        public NumberReader(FileInfo file)
        {
            arr = new byte[(Constants.UpperBound - Constants.LowerBound) + 1];
            arr = File.ReadAllBytes(file.FullName);
        }

        public IEnumerable<long> ReadIntegers()
        {
            foreach (Byte b in arr)
            {
                var value = b;
                yield return value;
            }
        }

        //Garbage collector should take care of this
        //for a Byte array automatically
        public void Dispose()
        {

        }
    }
}
