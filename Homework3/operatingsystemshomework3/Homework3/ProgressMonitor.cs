using System;
using System.Collections.Generic;
using System.Threading;

namespace Homework3 
{
    internal class ProgressMonitor
    {
        private readonly BoundBuffer<long> _boundBuffer;
        public long TotalCount = 0;

        public ProgressMonitor(List<long> results) 
        {
            //Dynamically allocates data for BB and sets results list as the BB's list.
             _boundBuffer = new BoundBuffer<long>();
            _boundBuffer.SetList(results);
        }

        public void Run() 
        {
            while (true)
            {
                Thread.Sleep(100);

                    var currentcount = _boundBuffer.GetList().Count;
                    TotalCount += currentcount;

                    _boundBuffer.GetList().Clear(); // clear out the current primes to save some memory

                   if (currentcount > 0)
                        Console.WriteLine("{0} primes found so far", TotalCount);
            }
        }
    }
}