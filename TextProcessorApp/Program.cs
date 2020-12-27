using System;
using System.IO;
using TextProcessorApp.Interfaces;
using TextProcessorApp.TextObjectModel;
using TextProcessorApp.TextParsing;
using TextProcessorApp.TextProcessing;

namespace TextProcessorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var txtInputFilePath = "input.txt";
            Text text;
            var textParser = new TextParser();
            using (var streamReader = new StreamReader(txtInputFilePath))
            {
                text = textParser.Parse(streamReader);
            }
            Console.WriteLine(text);
            var textFormatting = new TextProcessor();
             //textFormatting.GetUniqueWordsFromInterrogativeSentencesWithLength(text, 2);
            Text text1 = textFormatting.DeleteWordsStartsWithVowel(text, 5);
            Console.WriteLine(text1);
          
           
        }
    }
}
