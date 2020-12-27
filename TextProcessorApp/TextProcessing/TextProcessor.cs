using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using TextProcessorApp.Interfaces;
using TextProcessorApp.TextObjectModel;

namespace TextProcessorApp.TextProcessing
{
    public class TextProcessor
    {
        public IEnumerable<ISentence> SortSentencesAscending(Text text)
        {
            return text.Sentences.OrderBy(x =>x.WordsAmount);
        }

        public IEnumerable<ISentence> GetInterrogativeSentences(Text text)
        {
            return text.Sentences.Where(x => x.IsInterogative == true);
        }

        public IEnumerable<IWord> GetUniqueWordsFromSentence(ISentence sentence)
        {
            return sentence.SentenceElements.Where(x => x is IWord).Cast<IWord>().Distinct();
        }
        
        public void GetUniqueWordsFromInterrogativeSentencesWithLength(Text text, int length)
        {
            foreach(var sentence in GetInterrogativeSentences(text))
            {
                foreach(var word in GetUniqueWordsFromSentence(sentence).Where(x =>x.Length == length))
                {
                    Console.WriteLine(word);
                }
                Console.WriteLine("Next sentence");
            }
        }

        private ISentence RemoveWordsFromSentence(ISentence sentence, Predicate<IWord> predicate)
        {
            var newSentenceElements = sentence.SentenceElements.ToList();
            var matchingWords = FindMatchingWords(newSentenceElements, predicate);
            if (matchingWords.Any())
            {
                foreach (var element in matchingWords)
                {
                    var index = newSentenceElements.IndexOf(element);

                    if (index == newSentenceElements.Count - 2 && index > 0) index--;

                    newSentenceElements.Remove(element);

                    if (newSentenceElements.Count > 1) newSentenceElements.RemoveAt(index);
                }
            }
            return new Sentence(newSentenceElements);
        }
        
        public Text DeleteWordsStartsWithVowel(Text text, int lenght)
        {
            var editedSentences = text.Sentences.Select(x => RemoveWordsFromSentence(x,y =>y.Length == lenght && y.IsStartsWithVowel()))
                .Where(x =>x.SentenceElements.OfType<IWord>().Any()).ToList();
            return new Text(editedSentences);
        }
        private IList<IWord> FindMatchingWords<IWord>(IList<ISentenceElement> sentenceElements, Predicate<IWord> predicate)
        {
            return sentenceElements.OfType<IWord>().ToList().FindAll(predicate);
        }
       

    }
}
