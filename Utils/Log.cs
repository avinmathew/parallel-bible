using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibleDownload.Utils
{
    public class Log
    {
        public static void Info(string text)
        {
            Console.WriteLine(string.Format("{0} - {1}", DateTime.Now.ToShortTimeString(), text));
        }
    }
}
