using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WhoIsThatServer.Recognition.Recognition.RecUtils
{
    public static class RecUtil
    {
        public static MemoryStream GetStreamFromUri(string uri)
        {
            WebClient webClient = new WebClient();
            byte[] imageData = null;

            imageData = webClient.DownloadData(uri);

            return new MemoryStream(imageData);
        }
    }
}
