using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WallpaperAbyss
{
    /// <summary>
    /// Had issues with the normal Random implementation so needed a Threadsafe class to encapsulate it.
    /// </summary>
    static class ThreadSafeRandom
    {
        /// <summary>
        /// A random object that is thread safe. Eath thread gets its own instance
        /// </summary>
        [ThreadStatic] private static Random Local;

        /// <summary>
        /// A random object that is thread safe. Eath thread gets its own instance
        /// </summary>
        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}
