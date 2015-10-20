using System;
using System.Collections.Generic;
using System.Threading;

namespace Homework3 {
    internal class IsNumberPrimeCalculator {
        private readonly ICollection<long> _primeNumbers;
        private readonly Queue<long> _numbersToCheck;

        public IsNumberPrimeCalculator(ICollection<long> primeNumbers, Queue<long> numbersToCheck) {
            _primeNumbers = primeNumbers;
            _numbersToCheck = numbersToCheck;
        }

        public void CheckIfNumbersArePrime() 
        {
            while (true) 
            {

                if (!Spinlock.S.IsHeldByCurrentThread)
                    //Spinlock.S.Exit();
                    Spinlock.S.TryEnter(100, ref Spinlock.LockStatus);

                //queue is starting empty
                var numberToCheck = _numbersToCheck.Dequeue();

                if (IsNumberPrime(numberToCheck)) 
                {
                    //spinlock here to give time to process checks when possible  
                    
                        //unlock here to give time for I/O to go when nothing is ready to be checked     
                        //Spinlock.S.TryEnter(100, ref Spinlock.LockStatus);



                        _primeNumbers.Add(numberToCheck);

                        if (Spinlock.S.IsHeldByCurrentThread)
                            Spinlock.S.Exit();
                }
            }
        }

        private bool IsNumberPrime(long numberWeAreChecking) {
            const long firstNumberToCheck = 3;

            if (numberWeAreChecking % 2 == 0) {
                return false;
            }
            var lastNumberToCheck = Math.Sqrt(numberWeAreChecking);
            for (var currentDivisor = firstNumberToCheck; currentDivisor < lastNumberToCheck; currentDivisor += 2) {
                if (numberWeAreChecking % currentDivisor == 0) {
                    return false;
                }
            }
            return true;
        }
    }
}