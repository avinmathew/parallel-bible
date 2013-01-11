using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BibleDownload.Data;
using BibleDownload.Utils;

namespace BibleDownload
{
    public class MalayalamDownload
    {
        private static Dictionary<int, string> _books = new Dictionary<int, string>
        {
            {1,"ഉല്പത്തി"},
            {2,"പുറപ്പാടു്"},
            {3,"ലേവ്യപുസ്തകം"},
            {4,"സംഖ്യാപുസ്തകം"},
            {5,"ആവർത്തനം"},
            {6,"യോശുവ"},
            {7,"ന്യായാധിപന്മാർ"},
            {8,"രൂത്ത്"},
            {9,"1 ശമൂവേൽ"},
            {10,"2 ശമൂവേൽ"},
            {11,"1 രാജാക്കന്മാർ"},
            {12,"2 രാജാക്കന്മാർ"},
            {13,"1 ദിനവൃത്താന്തം"},
            {14,"2 ദിനവൃത്താന്തം"},
            {15,"എസ്രാ"},
            {16,"നെഹെമ്യാവു"},
            {17,"എസ്ഥേർ"},
            {18,"ഇയ്യോബ്"},
            {19,"സങ്കീർത്തനങ്ങൾ"},
            {20,"സദൃശ്യവാക്യങ്ങൾ"},
            {21,"സഭാപ്രസംഗി"},
            {22,"ഉത്തമ ഗീതം"},
            {23,"യെശയ്യാ"},
            {24,"യിരേമ്യാവു"},
            {25,"വിലാപങ്ങൾ"},
            {26,"യേഹേസ്കേൽ"},
            {27,"ദാനീയേൽ"},
            {28,"ഹോശേയ"},
            {29,"യോവേൽ"},
            {30,"ആമോസ്"},
            {31,"ഒബാദ്യാവു"},
            {32,"യോനാ"},
            {33,"മീഖാ"},
            {34,"നഹൂം"},
            {35,"ഹബക്കൂക്‍"},
            {36,"സെഫന്യാവു"},
            {37,"ഹഗ്ഗായി"},
            {38,"സെഖർയ്യാവു"},
            {39,"മലാഖി"},
            {40,"മത്തായി"},
            {41,"മർക്കൊസ്"},
            {42,"ലൂക്കോസ്"},
            {43,"യോഹന്നാൻ"},
            {44,"പ്രവൃത്തികൾ"},
            {45,"റോമർ"},
            {46,"1 കൊരിന്ത്യർ"},
            {47,"2 കൊരിന്ത്യർ"},
            {48,"ഗലാത്യർ"},
            {49,"എഫെസ്യർ"},
            {50,"ഫിലിപ്പിയർ"},
            {51,"കൊലൊസ്സ്യർ"},
            {52,"1 തെസ്സലൊനീക്യർ"},
            {53,"2 തെസ്സലൊനീക്യർ"},
            {54,"1 തിമൊഥെയൊസ്"},
            {55,"2 തിമൊഥെയൊസ്"},
            {56,"തീത്തൊസ്"},
            {57,"ഫിലേമോൻ"},
            {58,"എബ്രായർ"},
            {59,"യാക്കോബ്"},
            {60,"1 പത്രൊസ്"},
            {61,"2 പത്രൊസ്"},
            {62,"1 യോഹന്നാൻ"},
            {63,"2 യോഹന്നാൻ"},
            {64,"3 യോഹന്നാൻ"},
            {65,"യൂദാ"},
            {66,"വെളിപ്പാടു"}
        };

        public static Bible Run()
        {
            Bible bible = new Bible("മലയാളം");

            Regex regex = new Regex(@"{{verse\|(\d+)}}\s(.*)");

            foreach (var pair in _books)
            {
                int bookId = pair.Key;
                string name = pair.Value;
                int numberOfChapters = Book.ChaptersPerBook[bookId];

                Book book = new Book(bookId, name);
                bible.Books.Add(book);

                Log.Info("Book " + bookId);

                for (int i = 1; i < numberOfChapters + 1; i++)
                {
                    Log.Info(string.Format("Chapter {0}/{1}", i, numberOfChapters));

                    Chapter chapter = new Chapter(i);
                    book.Chapters.Add(chapter);

                    string url = string.Format("http://malayalambible.in/wiki/{0}:{1}.htm", bookId, i);
                    string page = Web.GetPage(url);
                    string[] lines = page.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < lines.Length; j++)
                    {
                        string line = lines[j];
                        Match match = regex.Match(line);
                        if (match.Success)
                        {
                            int verseId = Int32.Parse(match.Groups[1].Value);
                            string text = match.Groups[2].Value.Trim();
                            Verse verse = new Verse(verseId, text);
                            chapter.Verses.Add(verse);
                        }
                    }
                }
            }
            return bible;
        }
    }
}
