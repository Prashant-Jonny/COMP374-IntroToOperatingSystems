using System;
using System.Collections.Generic;

namespace Homework3
{
    internal class IsNumberPrimeCalculator
    {
        private readonly BoundBuffer<long> _boundBuffer; 

        public IsNumberPrimeCalculator(List<long> primeNumbers, Queue<long> numbersToCheck) 
        {
            //Dynamically allocated BB and sets list/queue as BB objects to keep them encapsulated
             _boundBuffer = new BoundBuffer<long>();
            _boundBuffer.SetList(primeNumbers);
            _boundBuffer.SetQueue(numbersToCheck);
        }

        public void CheckIfNumbersArePrime()
        {
            while (true)
            {
                //lock needed to not dequeue over maxSize with another thread
                lock (_boundBuffer.GetQueue())
                {
                    if (_boundBuffer.GetQueue().Count != 0)
                    {
                         var numberToCheck = _boundBuffer.GetQueue().Dequeue();

                        if (IsNumberPrime(numberToCheck))
                            _boundBuffer.GetList().Add(numberToCheck);
                    }
                }
            }
        }

        private static bool IsNumberPrime(long numberWeAreChecking) 
        {
            const long firstNumberToCheck = 3;

            if (numberWeAreChecking % 2 == 0) 
            {
                return false;
            }
            var lastNumberToCheck = Math.Sqrt(numberWeAreChecking);
            for (var currentDivisor = firstNumberToCheck; currentDivisor < lastNumberToCheck; currentDivisor += 2) 
            {
                if (numberWeAreChecking % currentDivisor == 0) 
                {
                    return false;
                }
            }
            return true;
        }
    }
}