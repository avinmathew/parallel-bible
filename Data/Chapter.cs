using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleDownload.Data
{
    public class Chapter
    {
        public int Id { get; set; }
        public List<Verse> Verses { get; set; }

        public Chapter() { }

        public Chapter(int id)
        {
            Id = id;
            Verses = new List<Verse>();
        }
    }
}
