using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Homework3
{
    internal class NumberReader : IDisposable
    {
        private readonly Byte[] _arr;

        public NumberReader(FileInfo file)
        {
            _arr = new byte[Constants.ArraySize];
            _arr = File.ReadAllBytes(file.FullName);
        }

        public Byte[] ReadIntegers()
        {
            return _arr;
        }

        //Garbage collector should take care of this
        //for a Byte array automatically
        public void Dispose(){}
    }
}
