using System;
using System.IO;

namespace Homework3
{
    internal class NumberReader : IDisposable
    {
        private readonly byte[] _arr;
        public NumberReader(FileSystemInfo file)
        {
             //Dynamically allocated byte array and reads everything in
            var lineCount = File.ReadAllLines(file.FullName).Length;
            _arr = new byte[lineCount];
                        Console.WriteLine("length: "+_arr.Length);

            _arr = File.ReadAllBytes(file.FullName);

            Console.WriteLine("First byte: {0}", _arr[0]);
            Console.WriteLine("Last byte: {0}", _arr[_arr.Length - 1]);
            Console.WriteLine("length: "+_arr.Length);
        }

        public byte[] ReadIntegers()
        {
            //instead of yield return and iterating through each number just returning an array also seems to work
            return _arr;
         }

        //Garbage collector should take care of this
        //for a Byte array automatically
        public void Dispose(){}

    }
}

