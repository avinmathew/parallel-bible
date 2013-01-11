using System;
using System.IO;
using BibleDownload.Data;
using BibleDownload.Utils;

namespace BibleDownload
{
    public class Program
    {
        private static string _defaultEnglishPath = "english.xml";
        private static string _defaultMalayalamPath = "malayalam.xml";
        private static string _defaultHindiPath = "hindi.xml";

        public static void Main(string[] args)
        {
            Bible english;
            if (File.Exists(_defaultEnglishPath))
            {
                Log.Info("Loading English bible");
                english = Bible.Load(_defaultEnglishPath);
            }
            else
            {
                Log.Info("Converting English bible");
                english = EnglishDownload.Run();
                english.Save(_defaultEnglishPath);
            }

            Bible malayalam;
            if (File.Exists(_defaultMalayalamPath))
            {
                Log.Info("Loading Malayalam bible");
                malayalam = Bible.Load(_defaultMalayalamPath);
            }
            else
            {
                Log.Info("Downloading Malayalam bible");
                malayalam = MalayalamDownload.Run();
                malayalam.Save(_defaultMalayalamPath);
            }

            Bible hindi;
            if (File.Exists(_defaultHindiPath))
            {
                Log.Info("Loading Hindi bible");
                hindi = Bible.Load(_defaultHindiPath);
            }
            else
            {
                Log.Info("Downloading Hindi bible");
                hindi = HindiDownload.Run();
                hindi.Save(_defaultHindiPath);
            }

            //english.Compare(malayalam);
            //english.Compare(hindi);
            //Log.Info("Comparison finished");

            GenerateHtml generate = new GenerateHtml(english, malayalam, hindi);
            generate.Run(Environment.CurrentDirectory);
        }
    }
}
