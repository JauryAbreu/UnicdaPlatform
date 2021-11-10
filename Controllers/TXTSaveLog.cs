using System;
using System.IO;

namespace UnicdaPlatform.Controllers
{
    public class TXTSaveLog
    {
        public static void WriteToTxtFile(string text)
        {
            try
            {
                #region PATH | RUTA
                string path = Environment.CurrentDirectory + "//Log//" + new Controllers.GenericFunctions().GetTimeZone().Month;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path += @"\" + new Controllers.GenericFunctions().GetTimeZone().ToString("ddMMyy") + ".txt";
                #endregion;

                #region Append Info
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(string.Format("{0} {1}", new Controllers.GenericFunctions().GetTimeZone().ToString("yyyy-MM-dd HH:mm:ss.fff"), text));

                    writer.Flush();
                    writer.Close();
                }
                #endregion
            }
            catch (Exception)
            {
            }
            finally
            {
                try { Directory.Delete(Environment.CurrentDirectory + "//Log//" + new Controllers.GenericFunctions().GetTimeZone().Date.AddMonths(-2).Month); } catch { }
            }
        }
    }
}
