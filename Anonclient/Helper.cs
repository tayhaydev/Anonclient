using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnonFile_Uploader
{
    class Helper
    {
        public static string ApiKey()
        {
            string url = "https://api.anonfile.com/upload?token=yourtoken";
            return url;
        }

        public static string Pars(string strSource, string strStart, string strEnd, int startPos = 0, string error = null)
        {
            string result;
            try
            {
                int length = strStart.Length;
                string text = "";
                int num = strSource.IndexOf(strStart, startPos);
                int num2 = strSource.IndexOf(strEnd, num + length);
                bool flag = num != -1 & num2 != -1;
                if (flag)
                {
                    text = strSource.Substring(num + length, num2 - (num + length));
                }
                result = text;
            }
            catch
            {
                result = error;
            }
            return result;
        }
    }
}
