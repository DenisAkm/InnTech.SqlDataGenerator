using System;

namespace InnTech.SqlDataGenerator
{
    public class DateTimeGenerator : ITypeGenerator
    {  

        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }

        private bool SetDateTimeNow { get; }

        public DateTimeGenerator(DateTime minDate, DateTime maxDate, bool setDateTimeNow)
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
            var value = SetDateTimeNow ? DateTime.UtcNow : (DateTime)GetRandom(column);

            return $"'{value:u}'";
        }
    }
}