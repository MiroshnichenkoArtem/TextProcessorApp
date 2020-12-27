using System;
using System.Collections.Generic;
using System.Text;

namespace TextProcessorApp.Interfaces
{
    public interface ISeparator : ISentenceElement
    {
        bool IsSpaceMark();
        bool IsWordSeparationMark();
        bool IsSentenceSeparationMark();
        bool IsQuestionMark();
    }
}
