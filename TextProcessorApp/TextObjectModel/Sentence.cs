using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TextProcessorApp.Interfaces;
using System.Linq;

namespace TextProcessorApp.TextObjectModel
{
    public class Sentence : ISentence
    {
        public bool IsInterogative
        {
            get
            {   //добавить эксепшн на пустой список
                return (SentenceElements.Last() as ISeparator).IsQuestionMark();           
            }
        }
        public int WordsAmount 
        {
            get
            {
                return SentenceElements.Where(x => x is IWord).Count();
            }
        }
        
        private ICollection<ISentenceElement> _sentenceElements;    
        public ICollection<ISentenceElement> SentenceElements
        {
            get
            {              
                return _sentenceElements;
            }

            private set
            {
                if (value.Count == 0)
                    throw new ArgumentException("Value cannot be an empty collection.", nameof(value));
                _sentenceElements = value;
            }
        }            
        public Sentence()
        {
            _sentenceElements = new Collection<ISentenceElement>();         
        }

        public Sentence(ICollection<ISentenceElement> sentenceElements) : this()
        {
            SentenceElements = sentenceElements;
        }
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var element in SentenceElements)
            {
                stringBuilder.Append(element);
            }

            return stringBuilder.ToString();
        }
    }
}
