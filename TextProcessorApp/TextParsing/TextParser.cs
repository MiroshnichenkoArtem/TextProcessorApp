using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextProcessorApp.Interfaces;
using TextProcessorApp.TextObjectModel;

namespace TextProcessorApp.TextParsing
{
    public class TextParser
    {
        private readonly string _wordSeparationPattern = @"\b(\w+)((\p{P}{0,3})\s?)";
        private readonly string _sentencesSeparationPattern = " ^\\s+[A-Za-z,;'\"\\s]+[.?!]$";

        public Text Parse(StreamReader streamReader)
        {
            var sentences = new List<ISentence>();
            var strBuffer = string.Empty;

            string fileLine;
            while ((fileLine = streamReader.ReadLine()) != null)
            {
                fileLine = strBuffer + Regex.Replace(fileLine, @"\s+", " ");

                var strSentences = Regex.Split(fileLine, _sentencesSeparationPattern)
                    .Select(x => string.Concat(x, " "));

                foreach (var strSentence in strSentences)
                {
                    if (SeparatorConstants.SentenceSeparationMarks.Any(x => strSentence.EndsWith(x)))
                    {
                        var elementsForNewSentence = Parse(strSentence);
                        if (IsItPossibleToCreateSentence(elementsForNewSentence))
                        {
                            sentences.Add(new Sentence(elementsForNewSentence));
                        }
                        strBuffer = string.Empty;
                    }
                    else
                    {
                        strBuffer = strSentence;
                    }
                }
            }

            if (strBuffer != string.Empty)
            {
                var elementsForNewSentence = Parse(strBuffer);
                if (IsItPossibleToCreateSentence(elementsForNewSentence))
                {
                    sentences.Add(new Sentence(elementsForNewSentence));
                }
              
            }

            return new Text(sentences);
        }

        public ICollection<ISentenceElement> Parse(string inputLine)
        {
            var line = string.Concat(inputLine, " ");

            var sentenceElements = new Collection<ISentenceElement>();

            var sentenceElementFactory = new SentenceElementFactory();
            foreach (Match match in Regex.Matches(line, _wordSeparationPattern))
            {
                sentenceElements.Add(sentenceElementFactory.GetSentenceElement(match.Groups[1].ToString()));

                sentenceElements.Add(sentenceElementFactory.GetSentenceElement(match.Groups[2].ToString()));
            }

            return sentenceElements;
        }

        private static bool IsItPossibleToCreateSentence(ICollection<ISentenceElement> sentenceElements)
        {
            return sentenceElements.OfType<IWord>().Any() && sentenceElements.OfType<ISeparator>().Any();
        }
    }
}
