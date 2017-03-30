using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikit.Shared
{
    public static class StringLocker
    {
        private static ConcurrentDictionary<string, object> _locks =
            new ConcurrentDictionary<string, object>();

        public static object GetLockObject(string s)
        {
            return _locks.GetOrAdd(s, k => new object());
        }
    }
}
