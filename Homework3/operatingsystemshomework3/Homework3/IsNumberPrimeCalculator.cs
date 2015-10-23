using System;
using System.Collections.Generic;
using System.Threading;

namespace Homework3 {
    internal class IsNumberPrimeCalculator {
        private readonly ICollection<long> _primeNumbers;
        private readonly Queue<long> _numbersToCheck;

        private SpinLock _s = new SpinLock();

        public IsNumberPrimeCalculator(ICollection<long> primeNumbers, Queue<long> numbersToCheck) {
            _primeNumbers = primeNumbers;
            _numbersToCheck = numbersToCheck;
        }

        public void CheckIfNumbersArePrime()
        {
            while (true)
            {
                Boolean _lockStatus = false;

                //lockStatus = S.IsHeld;

                if (_s.IsHeldByCurrentThread)
                    _s.Exit();

                if (_numbersToCheck.Count != 0)
                {
                    if (!_s.IsHeld)
                    {
                        _s.TryEnter(ref _lockStatus);

                        if (_s.IsHeldByCurrentThread)
                        {
                            var numberToCheck = _numbersToCheck.Dequeue();

                            if (IsNumberPrime(numberToCheck))
                            {
                                _primeNumbers.Add(numberToCheck);
                            }
                        }
                    }
                }
            }
        }

        private bool IsNumberPrime(long numberWeAreChecking) 
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