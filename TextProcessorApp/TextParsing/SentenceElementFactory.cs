using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextProcessorApp.Interfaces;
using TextProcessorApp.TextObjectModel;

namespace TextProcessorApp.TextParsing
{
    public class SentenceElementFactory
    {
        private IDictionary<string, ISentenceElement> _sentenceElements =
            new Dictionary<string, ISentenceElement>();

        public ISentenceElement GetSentenceElement(string key)
        {
            ISentenceElement sentenceElement;
            if (_sentenceElements.ContainsKey(key))
            {
                sentenceElement = _sentenceElements[key];
            }
            else
            {
                if (SeparatorConstants.AllSentenceSeparators.Contains(key))
                {
                    sentenceElement = new Separator(key);
                }
                else
                {
                    sentenceElement = new Word(key);
                }
                _sentenceElements.Add(key, sentenceElement);
            }

            return sentenceElement;
        }
    }
}
