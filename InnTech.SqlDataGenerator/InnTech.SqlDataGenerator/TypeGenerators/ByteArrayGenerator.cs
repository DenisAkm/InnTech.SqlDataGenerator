using System;

namespace InnTech.SqlDataGenerator
{
    public class ByteArrayGenerator: ITypeGenerator
    {
        private int ByteArraySize { get; }

        public ByteArrayGenerator(int byteArraySize)
        {
            ByteArraySize = byteArraySize;
        }

        public object GetRandom(EntityProperty column)
        {
            return Randomize.NextBytes(ByteArraySize);
        }

        public string GetValue(EntityProperty column)
        {
            var value = (byte[])GetRandom(column);
            return "0x" + BitConverter.ToString(value).Replace("-", "");
        }
    }
}