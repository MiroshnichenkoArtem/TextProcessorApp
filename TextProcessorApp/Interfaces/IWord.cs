using System;
using System.Collections.Generic;
using System.Text;

namespace TextProcessorApp.Interfaces
{
    public interface IWord : ISentenceElement
    {
        int Length { get;}
        bool IsStartsWithVowel();
        
    }
}
