using System;

namespace InnTech.SqlDataGenerator
{
    public class DateTimeOffsetGenerator: ITypeGenerator
    {
        public DateTimeOffset MinDate { get; set; }
        public DateTimeOffset MaxDate { get; set; }

        private bool SetDateTimeNow { get; }

        public DateTimeOffsetGenerator(DateTimeOffset minDate, DateTimeOffset maxDate, bool setDateTimeNow)
        {
            MinDate = minDate;
            MaxDate = maxDate;
            SetDateTimeNow = setDateTimeNow;
        }

        public object GetRandom(EntityProperty column)
        {
            var range = (MaxDate - MinDate).Days;
            return MinDate.AddDays(Randomize.Next(range));
        }

        public string GetValue(EntityProperty column)
        {
            var value = SetDateTimeNow ? DateTimeOffset.UtcNow : (DateTimeOffset)GetRandom(column);
            return $"'{value:u}'";
        }
    }
}