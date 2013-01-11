using System;
using System.Linq;
using System.Xml.Linq;
using BibleDownload.Data;

namespace BibleDownload
{
    public class EnglishDownload
    {
        public static Bible Run()
        {
            Bible bible = new Bible("English");

            XDocument doc = XDocument.Load("ESV.xml");
            var bookElements = doc.Root.Elements("b");
            for (int i = 1; i < bookElements.Count() + 1; i++)
            {
                var bookElement = bookElements.ElementAt(i - 1);
                string bookName = bookElement.Attribute("n").Value;
                Book book = new Book(i, bookName);
                bible.Books.Add(book);

                foreach (var chapterElement in bookElement.Elements("c"))
                {
                    int chapterId = Int32.Parse(chapterElement.Attribute("n").Value);
                    Chapter chapter = new Chapter(chapterId);
                    book.Chapters.Add(chapter);

                    foreach (var verseElement in chapterElement.Elements("v"))
                    {
                        int verseId = Int32.Parse(verseElement.Attribute("n").Value);
                        string text = verseElement.Value;
                        Verse verse = new Verse(verseId, text);
                        chapter.Verses.Add(verse);
                    }
                }
            }

            return bible;
        }
    }
}
