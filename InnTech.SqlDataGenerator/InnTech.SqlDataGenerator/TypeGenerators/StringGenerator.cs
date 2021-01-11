using System.Collections.Generic;
using System.Text;

namespace InnTech.SqlDataGenerator
{
    public class StringGenerator : ITypeGenerator
    {
        private int WordLength { get; }
        private string Vowels { get; } = "aeijouy";

        public bool UseVocabulary { get; set; }
        private string Consonants { get; } = "bcdfghklmnpqrstvwxz";

        private List<string> Vocabulary;

        public StringGenerator(List<string> vocabulary = null, int wordLength = 255, bool useVocabulary = true)
        {
            UseVocabulary = useVocabulary;
            WordLength = wordLength;

            if (vocabulary != null && vocabulary.Count > 0)
            {
                Vocabulary = vocabulary;
            }
            else
            {
                UseVocabulary = false;
            }
        }

        public object GetRandom(EntityProperty column)
        {
            if (UseVocabulary)
            {
                var aWord = Vocabulary[Randomize.Next(Vocabulary.Count)];

                if (aWord.Length > WordLength)
                {
                    aWord = aWord.Remove(WordLength);
                }

                return aWord;
            }
            else
            {
                var text = new StringBuilder(Consonants[Randomize.Next(Consonants.Length)].ToString().ToUpper());
                for (var i = 0; i < WordLength - 1; i++)
                {
                    text.Append(i % 2 == 0
                        ? Vowels[Randomize.Next(Vowels.Length)]
                        : Consonants[Randomize.Next(Consonants.Length)]);
                }
                return text.ToString();
            }
        }

        public string GetValue(EntityProperty column)
        {
            return $"'{GetRandom(column)}'";
        }
    }
}