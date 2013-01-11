using System;
using System.IO;
using System.Net;
using System.Threading;

namespace BibleDownload.Utils
{
    public class Web
    {
        public static readonly int NUMBER_OF_RETRIES = 5;

        public static string GetPage(string url)
        {
            int times = 0;
        Start:
            try
            {
                WebRequest objRequest = HttpWebRequest.Create(url);
                WebResponse objResponse = objRequest.GetResponse();
                using (StreamReader reader = new StreamReader(objResponse.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }

            }
            catch (Exception)
            {
                times++;
                if (times < NUMBER_OF_RETRIES)
                {
                    Log.Info("Retrying...");
                    Thread.Sleep(3000);
                    goto Start;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
