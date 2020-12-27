using System;
using System.Collections.Generic;
using System.Text;
using TextProcessorApp.Interfaces;

namespace TextProcessorApp.TextObjectModel
{
    public abstract  class SentenceElement: ISentenceElement
    {
        public string Chars { get; set; }

        protected SentenceElement(string str)
        {
            Chars = str;
        }
    }
}
