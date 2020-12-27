using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextProcessorApp.Interfaces;

namespace TextProcessorApp.TextObjectModel
{
    public class Separator : SentenceElement, ISeparator
    {
        public Separator(string str) : base(str)
        {
            Chars = str;
        }

        public bool IsSpaceMark()
        {
            return Chars.Equals(SeparatorConstants.Space);
        }

        public bool IsWordSeparationMark()
        {
            return SeparatorConstants.WordSeparationMarks.Any(x => Chars.Equals(x));
        }
        public bool IsSentenceSeparationMark()
        {
            return SeparatorConstants.SentenceSeparationMarks.Any(x => Chars.Equals(x));
        }
        public bool IsQuestionMark()
        {
            return Chars.Contains('?');
        }

        public override string ToString()
        {
            return Chars;
        }
    }
}
