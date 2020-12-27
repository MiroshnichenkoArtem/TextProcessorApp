using System;
using System.Collections.Generic;
using System.Text;

namespace TextProcessorApp.Interfaces
{
    public interface ISentence
    {
        ICollection<ISentenceElement> SentenceElements { get; }
        bool IsInterogative { get; }
        int WordsAmount { get; }
        public void ReplaceWordsWithSubstring(int length, string substring);


    }
}
