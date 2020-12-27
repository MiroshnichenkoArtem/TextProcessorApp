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
        //public ICollection<ISentenceElement> ReplaceWord(ISentence sentence, Predicate<IWord> predicate,
        //   ICollection<ISentenceElement> sentenceElements)
        //{
        //    var newSentenceElements = sentence.SentenceElements.ToList();
        //    var matchingWords = FindMatchingWords(newSentenceElements, predicate);
        //    if (matchingWords.Any())
        //    {
        //        foreach (var element in matchingWords)
        //        {
        //            var index = newSentenceElements.IndexOf(element);

        //            newSentenceElements.Remove(element);

        //            newSentenceElements.RemoveAt(index);

        //            newSentenceElements.InsertRange(index, sentenceElements);
        //        }
        //    }

        //    return newSentenceElements.Count != 0 ? new List<ISentenceElement>(newSentenceElements) : null;
        //}
        //public Text ReplacesWordsInSentenceWithSubstring(Text text, int sentenceNumber, int wordLength,
        //   ICollection<ISentenceElement> sentenceElements)
        //{
        //    var sentenceIndex = sentenceNumber - 1;

        //    var sentencesForNewText = new List<ISentence>();
        //    var elementsForNewSentences = new List<ISentenceElement>();
        //    var elementsForOneNewSentence = new List<ISentenceElement>();

        //    elementsForNewSentences.AddRange(ReplaceWord(text.Sentences[sentenceIndex],
        //        x => x.Length == wordLength, sentenceElements));

        //    // If it is true, then no words were found for deletion.
        //    if (text.Sentences[sentenceIndex].SentenceElements.Count != elementsForNewSentences.Count)
        //    {
        //        foreach (var sentenceElement in elementsForNewSentences)
        //        {
        //            elementsForOneNewSentence.Add(sentenceElement);

        //            if (!(sentenceElement is ISeparator separator) || !separator.IsSentenceSeparationMark()) continue;

        //            sentencesForNewText.Add(new Sentence(elementsForOneNewSentence.ToList()));

        //            elementsForOneNewSentence.Clear();
        //        }
        //    }

        //    if (elementsForOneNewSentence.Count == 0)
        //    {
        //        return new Text(AddSentencesToTextByIndex(text, sentenceIndex, sentencesForNewText));
        //    }

        //    var nextSentenceIndex = sentenceIndex + 1;

        //    elementsForOneNewSentence.AddRange(text.Sentences[nextSentenceIndex].SentenceElements);

        //    sentencesForNewText.Add(new Sentence(elementsForOneNewSentence.ToList()));

        //    text.Sentences.RemoveAt(sentenceIndex);
        //    text.Sentences.RemoveAt(sentenceIndex);

        //    return new Text(AddSentencesToTextByIndex(text, sentenceIndex, sentencesForNewText));
        //}
        //private IList<ISentence> AddSentencesToTextByIndex(Text text, int sentenceIndex,
        //   ICollection<ISentence> sentences)
        //{
        //    var newTextSentences = text.Sentences.ToList();

        //    newTextSentences.InsertRange(sentenceIndex, sentences);

        //    return new List<ISentence>(newTextSentences);
        //}


    }
}
