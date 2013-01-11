using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibleDownload.Data;
using RazorEngine;
using BibleDownload.Utils;
using System.IO;

namespace BibleDownload
{
    public class GenerateHtml
    {
        private static readonly string _indexTemplate = "Templates/Index.cshtml";
        private static readonly string _versesTemplate = "Templates/Verses.cshtml";

        private Bible _english;
        private Bible _malayalam;
        private Bible _hindi;

        public GenerateHtml(Bible english, Bible malayalam, Bible hindi)
        {
            _english = english;
            _malayalam = malayalam;
            _hindi = hindi;
        }

        public void Run(string path)
        {
            GenerateIndex(path);
            GenerateVerses(path);
        }

        private void GenerateIndex(string path)
        {
            Log.Info("Generating index");

            var oldTestamentBooks = _english.Books.Take(39).Select(b => new BookView(b.Id.ToString(), b.Name, Book.ChaptersPerBook[b.Id]));
            var newTestamentBooks = _english.Books.Skip(39).Select(b => new BookView(b.Id.ToString(), b.Name, Book.ChaptersPerBook[b.Id]));

            BookListView model = new BookListView(oldTestamentBooks.ToList(), newTestamentBooks.ToList());
            string template = File.ReadAllText(_indexTemplate);
            string result = Razor.Parse(template, model);

            File.WriteAllText(path + Path.DirectorySeparatorChar + "index.html", result);
        }

        private void GenerateVerses(string path)
        {
            Razor.Compile(_versesTemplate, "versesTemplate");

            for (int i = 1; i < _english.Books.Count + 1; i++)
            {
                string bookPath = path + Path.DirectorySeparatorChar + i;
                Directory.CreateDirectory(bookPath);

                Book englishBook = _english.Books[i - 1];
                Book malayalamBook = _malayalam.Books[i - 1];
                Book hindiBook = _hindi.Books[i - 1];

                Log.Info("Generating " + englishBook.Name);

                for (int j = 1; j < englishBook.Chapters.Count + 1; j++)
                {
                    string chapterPath = bookPath + Path.DirectorySeparatorChar + j;
                    Directory.CreateDirectory(chapterPath);

                    Chapter englishChapter = englishBook.Chapters[j - 1];
                    Chapter malayalamChapter = malayalamBook.Chapters[j - 1];
                    Chapter hindiChapter = hindiBook.Chapters[j - 1];

                    BookChapter bookChapter = new BookChapter(englishBook.Name + " " + englishChapter.Id);

                    if (englishBook.Id == 1 && englishChapter.Id == 1)
                    {
                        bookChapter.HasBack = false;
                    }
                    else
                    {
                        int backBook = englishBook.Id;
                        int backChapter = englishChapter.Id - 1;
                        if (backChapter == 0)
                        {
                            backBook--;
                            backChapter = Book.ChaptersPerBook[backBook];
                        }
                        bookChapter.BackPath = string.Format("/{0}/{1}", backBook, backChapter);
                    }
                    if (englishBook.Id == 66 && englishChapter.Id == 22)
                    {
                        bookChapter.HasForward = false;
                    }
                    else
                    {
                        int forwardBook = englishBook.Id;
                        int forwardChapter = englishChapter.Id + 1;
                        if (forwardChapter > Book.ChaptersPerBook[forwardBook])
                        {
                            forwardBook++;
                            forwardChapter = 1;
                        }
                        bookChapter.ForwardPath = string.Format("/{0}/{1}", forwardBook, forwardChapter);
                    }

                    int maxVerses = Math.Max(Math.Max(englishChapter.Verses.Count, malayalamChapter.Verses.Count), hindiChapter.Verses.Count);
                    for (int k = 0; k < maxVerses; k++)
                    {
                        BookChapterVerse verse = new BookChapterVerse();
                        if (k < englishChapter.Verses.Count)
                        {
                            verse.InEnglish = true;
                            verse.English = englishChapter.Verses[k];
                        }
                        if (k < malayalamChapter.Verses.Count)
                        {
                            verse.InMalayalam = true;
                            verse.Malayalam = malayalamChapter.Verses[k];
                        }
                        if (k < hindiChapter.Verses.Count)
                        {
                            verse.InHindi = true;
                            verse.Hindi = hindiChapter.Verses[k];
                        }
                        bookChapter.Verses.Add(verse);
                    }

                    string template = File.ReadAllText(_versesTemplate);
                    string result = Razor.Parse(template, bookChapter);

                    //string result = Razor.Run("versesTemplate", bookChapter);

                    File.WriteAllText(chapterPath + Path.DirectorySeparatorChar + "index.html", result);
                }
            }
        }
    }

    public class BookView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Chapters { get; set; }

        public BookView(string id, string name, int chapters)
	    {
            Id = id;
            Name = name;
            Chapters = chapters;
	    }
    }

    public class BookListView
    {
        public List<BookView> OldTestament { get; set; }
        public List<BookView> NewTestament { get; set; }

        public BookListView(List<BookView> oldTestament, List<BookView> newTestament)
        {
            OldTestament = oldTestament;
            NewTestament = newTestament;
        }
    }

    public class BookChapter
    {
        public string Name { get; set; }
        public List<BookChapterVerse> Verses { get; set; }
        public bool HasBack { get; set;}
        public bool HasForward { get; set; }
        public string BackPath { get; set; }
        public string ForwardPath { get; set; }

        public BookChapter(string name)
        {
            Name = name;
            Verses = new List<BookChapterVerse>();
            HasBack = true;
            HasForward = true;
        }
    }

    public class BookChapterVerse
    {
        public bool InEnglish { get; set; }
        public Verse English { get; set; }
        public bool InMalayalam { get; set; }
        public Verse Malayalam { get; set; }
        public bool InHindi { get; set; }
        public Verse Hindi { get; set; }
    }
}
