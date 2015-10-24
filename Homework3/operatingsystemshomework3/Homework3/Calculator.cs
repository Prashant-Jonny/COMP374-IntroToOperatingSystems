﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Homework3 {
    internal class Calculator {
        private Queue<long> _numbersToCheck ;
        private List<long> _results; 

        public void Run(NumberReader reader) 
        {
            _numbersToCheck = new Queue<long>();
            _results = new List<long>();

            lock (_numbersToCheck)
            {
                StartComputationThreads(_results, _numbersToCheck);
            }

            var progressMonitor = new ProgressMonitor(_results);

            new Thread(progressMonitor.Run) {IsBackground = true}.Start();

            foreach (var value in reader.ReadIntegers())
            {
                lock (_numbersToCheck)
                {
                    _numbersToCheck.Enqueue(value);
                }
            }            
                while (_numbersToCheck.Count > 0)
                {
                    Thread.Sleep(100);
                }
            

            Console.WriteLine("{0} of the numbers were prime", progressMonitor.TotalCount);
        }

        private static void StartComputationThreads(List<long> results, Queue<long> numbersToCheck) {
            var threads = CreateThreads(results, numbersToCheck);
            threads.ForEach(thread => thread.Start());
        }
        
        private static List<Thread> CreateThreads(List<long> results, Queue<long> numbersToCheck) {
            var threadCount = Environment.ProcessorCount*2;

            Console.WriteLine("Using {0} compute threads and 1 I/O thread", threadCount);

            var threads =
                (from threadNumber in Sequence.Create(0, threadCount)
                    let calculator = new IsNumberPrimeCalculator(results, numbersToCheck)
                    let newThread =
                        new Thread(calculator.CheckIfNumbersArePrime) {
                            IsBackground = true,
                            Priority = ThreadPriority.BelowNormal
                        }
                    select newThread).ToList();
            return threads;
        }
    }
}