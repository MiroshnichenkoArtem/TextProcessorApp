using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using TextProcessorApp.Interfaces;

namespace TextProcessorApp.TextObjectModel
{
    public class Word : SentenceElement, IWord, IEquatable<Word>
    {
       public int Length
       {
            get
            {
                return Chars.Length;
            }
       }

        public Word(string str) : base(str)
        {
            Chars = str;
        }

        public bool IsStartsWithVowel()
        {
            var vowels = new[] { 'a', 'e', 'i', 'o', 'u', 'y' };
            return vowels.Any(x => x == Chars.ToLower().First());
        }
        public override string ToString()
        {
            return Chars;
        }

        public bool Equals(Word other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (Object.ReferenceEquals(this, other))
                return true;

            return Chars.Equals(other.Chars);     
        }
    }
}
