using System;
using System.Collections.Generic;
using System.Threading;

namespace Homework3
{
    internal class IsNumberPrimeCalculator
    {
        private readonly  List<long> _primeNumbers;
        private readonly  Queue<long> _numbersToCheck;

        private SpinLock _s = new SpinLock();
        private Boolean _lockStatus;

        public IsNumberPrimeCalculator(List<long> primeNumbers, Queue<long> numbersToCheck) 
        {
            BoundBuffer<long> boundBuffer = new BoundBuffer<long>();
            boundBuffer.SetList(primeNumbers);
            boundBuffer.SetQueue(numbersToCheck);

            _primeNumbers = boundBuffer.GetList();
            _numbersToCheck = boundBuffer.GetQueue();
        }

        public void CheckIfNumbersArePrime()
        {
            while (true)
            {
                _lockStatus = false;
                if (_s.IsHeldByCurrentThread)
                    _s.Exit();

                lock (_numbersToCheck)
                {

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