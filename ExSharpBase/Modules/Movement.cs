using System;
using System.Collections.Generic;
using System.Drawing;

namespace ExSharpBase.Modules
{
    public class Movement
    {
        public TimeSpan Delay;
        public IEnumerable<Point> Points;

        public Movement(TimeSpan timeSpan, IEnumerable<Point> enumerable)
        {
            this.Delay = timeSpan;
            this.Points = enumerable;
        }
    }
}