using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BibleDownload.Data;
using BibleDownload.Utils;

namespace BibleDownload
{
    public class HindiDownload
    {
        private static List<Tuple<int, string, string>> _books = new List<Tuple<int, string, string>>
        {
            Tuple.Create(1,"http://gospelgo.com/a/hindi/gen.htm","उत्पत्ति"),
            Tuple.Create(2,"http://gospelgo.com/a/hindi/exo.htm","निर्गमन"),
            Tuple.Create(3,"http://gospelgo.com/a/hindi/lev.htm","लैव्यवस्था"),
            Tuple.Create(4,"http://gospelgo.com/a/hindi/num.htm","गिनती"),
            Tuple.Create(5,"http://gospelgo.com/a/hindi/deu.htm","व्यवस्थाविवरण"),
            Tuple.Create(6,"http://gospelgo.com/a/hindi/jos.htm","यहोशू"),
            Tuple.Create(7,"http://gospelgo.com/a/hindi/jdg.htm","न्यायियों"),
            Tuple.Create(8,"http://gospelgo.com/a/hindi/rut.htm","रूत"),
            Tuple.Create(9,"http://gospelgo.com/a/hindi/1sa.htm","1 शमूएल"),
            Tuple.Create(10,"http://gospelgo.com/a/hindi/2sa.htm","2 शमूएल"),
            Tuple.Create(11,"http://gospelgo.com/a/hindi/1ki.htm","1 राजा"),
            Tuple.Create(12,"http://gospelgo.com/a/hindi/2ki.htm","2 राजा"),
            Tuple.Create(13,"http://gospelgo.com/a/hindi/1ch.htm","1 इतिहास"),
            Tuple.Create(14,"http://gospelgo.com/a/hindi/2ch.htm","2 इतिहास"),
            Tuple.Create(15,"http://gospelgo.com/a/hindi/ezr.htm","एज्रा"),
            Tuple.Create(16,"http://gospelgo.com/a/hindi/neh.htm","नहेमायाह"),
            Tuple.Create(17,"http://gospelgo.com/a/hindi/est.htm","एस्तेर"),
            Tuple.Create(18,"http://gospelgo.com/a/hindi/job.htm","अय्यूब"),
            Tuple.Create(19,"http://gospelgo.com/a/hindi/psa.htm","भजन संहिता"),
            Tuple.Create(20,"http://gospelgo.com/a/hindi/pro.htm","नीतिवचन"),
            Tuple.Create(21,"http://gospelgo.com/a/hindi/ecc.htm","सभोपदेशक"),
            Tuple.Create(22,"http://gospelgo.com/a/hindi/son.htm","श्रेष्ठगीत"),
            Tuple.Create(23,"http://gospelgo.com/a/hindi/isa.htm","यशायाह"),
            Tuple.Create(24,"http://gospelgo.com/a/hindi/jer.htm","यिर्मयाह"),
            Tuple.Create(25,"http://gospelgo.com/a/hindi/lam.htm","विलापगीत"),
            Tuple.Create(26,"http://gospelgo.com/a/hindi/eze.htm","यहेजकेल"),
            Tuple.Create(27,"http://gospelgo.com/a/hindi/dan.htm","दानिय्येल"),
            Tuple.Create(28,"http://gospelgo.com/a/hindi/hos.htm","होशे"),
            Tuple.Create(29,"http://gospelgo.com/a/hindi/joe.htm","योएल"),
            Tuple.Create(30,"http://gospelgo.com/a/hindi/amo.htm","आमोस"),
            Tuple.Create(31,"http://gospelgo.com/a/hindi/oba.htm","ओबद्दाह"),
            Tuple.Create(32,"http://gospelgo.com/a/hindi/jon.htm","योना"),
            Tuple.Create(33,"http://gospelgo.com/a/hindi/mic.htm","मीका"),
            Tuple.Create(34,"http://gospelgo.com/a/hindi/nah.htm","नहूम"),
            Tuple.Create(35,"http://gospelgo.com/a/hindi/hab.htm","हबक्कूक"),
            Tuple.Create(36,"http://gospelgo.com/a/hindi/zep.htm","सपन्याह"),
            Tuple.Create(37,"http://gospelgo.com/a/hindi/hag.htm","हाग्गै"),
            Tuple.Create(38,"http://gospelgo.com/a/hindi/zec.htm","जकर्याह"),
            Tuple.Create(39,"http://gospelgo.com/a/hindi/mal.htm","मलाकी"),
            Tuple.Create(40,"http://gospelgo.com/a/hindi/mat.htm","मत्ती"),
            Tuple.Create(41,"http://gospelgo.com/a/hindi/mar.htm","मरकुस"),
            Tuple.Create(42,"http://gospelgo.com/a/hindi/luk.htm","लूका"),
            Tuple.Create(43,"http://gospelgo.com/a/hindi/joh.htm","यूहन्ना"),
            Tuple.Create(44,"http://gospelgo.com/a/hindi/act.htm","्रेरितों के काम"),
            Tuple.Create(45,"http://gospelgo.com/a/hindi/rom.htm","रोमियो"),
            Tuple.Create(46,"http://gospelgo.com/a/hindi/1co.htm","1 कुरिन्थियों"),
            Tuple.Create(47,"http://gospelgo.com/a/hindi/2co.htm","2 कुरिन्थियों"),
            Tuple.Create(48,"http://gospelgo.com/a/hindi/gal.htm","गलातियों"),
            Tuple.Create(49,"http://gospelgo.com/a/hindi/eph.htm","इफिसियों"),
            Tuple.Create(50,"http://gospelgo.com/a/hindi/phi.htm","फिलिप्पियों"),
            Tuple.Create(51,"http://gospelgo.com/a/hindi/col.htm","कुलुस्सियों"),
            Tuple.Create(52,"http://gospelgo.com/a/hindi/1th.htm","1 थिस्सलुनीकियों"),
            Tuple.Create(53,"http://gospelgo.com/a/hindi/2th.htm","2 थिस्सलुनीकियों"),
            Tuple.Create(54,"http://gospelgo.com/a/hindi/1ti.htm","1 तीमुथियुस"),
            Tuple.Create(55,"http://gospelgo.com/a/hindi/2ti.htm","2 तीमुथियुस  "),
            Tuple.Create(56,"http://gospelgo.com/a/hindi/tit.htm","तीतुस"),
            Tuple.Create(57,"http://gospelgo.com/a/hindi/phm.htm","फिलेमोन"),
            Tuple.Create(58,"http://gospelgo.com/a/hindi/heb.htm","इब्रानियों"),
            Tuple.Create(59,"http://gospelgo.com/a/hindi/jam.htm","याकूब"),
            Tuple.Create(60,"http://gospelgo.com/a/hindi/1pe.htm","1 पतरस"),
            Tuple.Create(61,"http://gospelgo.com/a/hindi/2pe.htm","2 पतरस"),
            Tuple.Create(62,"http://gospelgo.com/a/hindi/1jo.htm","1 यूहन्ना"),
            Tuple.Create(63,"http://gospelgo.com/a/hindi/2jo.htm","2 यूहन्ना"),
            Tuple.Create(64,"http://gospelgo.com/a/hindi/3jo.htm","3 यूहन्ना"),
            Tuple.Create(65,"http://gospelgo.com/a/hindi/jud.htm","यहूदा"),
            Tuple.Create(66,"http://gospelgo.com/a/hindi/rev.htm","प्रकाशित वाक्य")
        };

        public static Bible Run()
        {
            Bible bible = new Bible("हिन्दी बाइबिल");

            Regex regex = new Regex(@"<p><a>.*(\d+)</a><p>");

            foreach (var tuple in _books)
            {
                int bookId = tuple.Item1;
                string url = tuple.Item2;
                string name = tuple.Item3;

                Book book = new Book(bookId, name);
                bible.Books.Add(book);

                string page = Web.GetPage(url);
                string[] lines = page.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        int chapterId = Int32.Parse(match.Groups[1].Value);
                        Chapter chapter = new Chapter(chapterId);
                        book.Chapters.Add(chapter);

                        // Next line is the verses
                        i++;
                        string versesLine = lines[i];
                        string[] versesSplit = versesLine.Split(new string[] { "<b>", "</b>" }, StringSplitOptions.RemoveEmptyEntries);
                        var verses = versesSplit.Slice(2)
                                                .Select(slice => new Verse(Int32.Parse(slice.ElementAt(0)), slice.ElementAt(1).Trim()));
                        chapter.Verses.AddRange(verses);
                    }
                }
            }
            return bible;
        }
    }
}
