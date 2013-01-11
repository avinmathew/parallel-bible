using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using BibleDownload.Utils;

namespace BibleDownload.Data
{
    public class Bible
    {
        public string Language { get; set; }
        public List<Book> Books { get; set; }

        public Bible() { }

        public Bible(string language)
        {
            Language = language;
            Books = new List<Book>();
        }

        public void Save(string path)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Bible));
                serializer.Serialize(writer, this);
            }
        }

        public static Bible Load(string path)
        {
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Bible));
                return (Bible)serializer.Deserialize(reader);
            }
        }

        public void Compare(Bible bible2)
        {
            if (this.Books.Count != bible2.Books.Count)
            {
                Log.Info(string.Format("Books: {0} vs {1}", this.Books.Count, bible2.Books.Count));
            }
            for (int i = 0; i < this.Books.Count; i++)
            {
                var book1 = this.Books[i];
                var book2 = bible2.Books[i];
                if (book1.Chapters.Count != book2.Chapters.Count)
                {
                    Log.Info(string.Format("Chapters: {0} vs {1} in Book {2}", book1.Chapters.Count, book2.Chapters.Count, book1.Id));
                }
                for (int j = 0; j < book1.Chapters.Count; j++)
                {
                    var chapter1 = book1.Chapters[j];
                    var chapter2 = book2.Chapters[j];
                    if (chapter1.Verses.Count != chapter2.Verses.Count)
                    {
                        Log.Info(string.Format("Verses: {0} vs {1} in Book {2}, Chapter {3}", chapter1.Verses.Count, chapter2.Verses.Count, book1.Id, chapter1.Id));
                    }
                    for (int k = 0; k < chapter1.Verses.Count; k++)
                    {
                        var verse1 = chapter1.Verses[k];
                        if (string.IsNullOrEmpty(verse1.Text))
                        {
                            Log.Info(string.Format("Empty verses in Bible 1, Book {0}, Chapter {1}, Verse {2}", book1.Id, chapter1.Id, verse1.Id));
                        }
                        if (k < chapter2.Verses.Count)
                        {
                            var verse2 = chapter2.Verses[k];
                            if (string.IsNullOrEmpty(verse2.Text))
                            {
                                Log.Info(string.Format("Empty verses in Bible 2, Book {0}, Chapter {1}, Verse {2}", book2.Id, chapter2.Id, verse2.Id));
                            }
                        }
                    }
                }
            }
        }
    }
}
