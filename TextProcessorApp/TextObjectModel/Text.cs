using System;
using System.Collections.Generic;
using System.Text;
using TextProcessorApp.Interfaces;

namespace TextProcessorApp.TextObjectModel
{
    public class Text
    {
        private IList<ISentence> _sentences;

        public IList<ISentence> Sentences
        {
            get => _sentences;
            private set
            {
                if (value.Count == 0)
                    throw new ArgumentException("Value cannot be an empty collection.", nameof(value));

                _sentences = value;
            }
        }

        public Text()
        {
            Sentences = new List<ISentence>();
        }

        public Text(IList<ISentence> sentences)
        {
            Sentences = sentences;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var item in Sentences)
            {
                stringBuilder.Append(item);
            }

            return stringBuilder.ToString();
        }
    }
}
