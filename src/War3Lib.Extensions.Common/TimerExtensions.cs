using System;

using War3Net.CodeAnalysis.Common;

using static War3Api.Common;

namespace War3Lib.Extensions.Common
{
    [NativeLuaMemberContainer]
    public static class TimerExtensions
    {
        [NativeLuaMember("TimerStart")]
        public static extern void Start(this timer whichTimer, float timeout, bool periodic, Action handlerFunc);
    }
}