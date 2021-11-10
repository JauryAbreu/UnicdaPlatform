using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;

namespace UnicdaPlatform.Controllers
{
    public class GenericFunctions
    {
        public string UploadImage(IWebHostEnvironment hostEnvironment, string masterId, 
                                  string url, IFormFile file, int line = 0, bool manyImage = false)
        {
            string path = string.Empty;

            string ext = Path.GetExtension(file.FileName);
            if (manyImage)
                path = string.Format("{0}/{1}_{2}{3}", url, masterId, line.ToString("000"), ext);
            else
                path = string.Format("{0}/{1}{2}", url, masterId, ext);

            string serverFolder = Path.Combine(hostEnvironment.WebRootPath, path);

            string urlPath = Path.Combine(hostEnvironment.WebRootPath, url);
            if (!Directory.Exists(urlPath))
                Directory.CreateDirectory(urlPath);

            file.CopyTo(new FileStream(serverFolder, FileMode.Create));

            Thread.Sleep(100);
            return string.Format("~/{0}", path);
        }

        public DateTime GetTimeZone() 
        {
            try
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "SA Western Standard Time");
            }
            catch 
            {
                return DateTime.Now;
            }
        }
    }
}
