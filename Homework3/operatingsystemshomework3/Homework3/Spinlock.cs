using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homework3
{
    public static class Spinlock
    {
        public static SpinLock s = new SpinLock();
        public static bool LockStatus = s.IsHeld;
    }
}


