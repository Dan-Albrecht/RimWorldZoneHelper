namespace RimWorldZoneHelper
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Verse;

    internal class Helpers
    {
        private static readonly ConcurrentDictionary<int, object> hashes = new ConcurrentDictionary<int, object>();

        public static void LogOnce(string message)
        {
            // Good enough
            if (hashes.TryAdd(message.GetHashCode(), null))
            {
                Log.Message(message);
            }
        }
    }
}
