using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleDownload.Data
{
    public class Verse
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public Verse() { }
        
        public Verse(int id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}
