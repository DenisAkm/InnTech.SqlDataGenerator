using System;

namespace InnTech.SqlDataGenerator
{
    public class TimeSpanGenerator : ITypeGenerator
    {
        public TimeSpan MaxTime { get; set; }

        public TimeSpan MinTime { get; set; }

        public TimeSpanGenerator(TimeSpan maxTime, TimeSpan minTime)
        {
            MaxTime = maxTime;
            MinTime = minTime;
        }

        public object GetRandom(EntityProperty column)
        {
            var hour = Randomize.Next(MinTime.Hours, MaxTime.Hours);
            var minute = Randomize.Next(MinTime.Minutes, MaxTime.Minutes);
            var second = Randomize.Next(MinTime.Seconds, MaxTime.Seconds);

            return new TimeSpan(hour, minute, second);
        }

        public string GetValue(EntityProperty column)
        {
            return $"'{GetRandom(column)}'";
        }
    }
}
