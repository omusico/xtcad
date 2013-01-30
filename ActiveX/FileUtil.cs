using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ActiveX
{
    public class FileUtil
    {
        /// <summary>
        /// 从URL地址下载文件到本地磁盘
        /// </summary>
        /// <param name="ToLocalPath">本地磁盘地址</param>
        /// <param name="Url">URL网址</param>
        /// <returns></returns>
        public static long SaveFileFromUrl(string FileName, string Url)
        {
            long Value = 0;

            WebResponse response = null;
            Stream stream = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                response = request.GetResponse();
                stream = response.GetResponseStream();

                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    SaveBinaryFile(response, FileName);

                    Value = response.ContentLength;

                }

            }
            catch (Exception err)
            {
                Value = 0;
                string aa = err.ToString();
            }
            return Value;
        }
        /// <summary>
        /// Save a binary file to disk.
        /// </summary>
        /// <param name="response">The response used to save the file</param>
        // 将二进制文件保存到磁盘
        private static bool SaveBinaryFile(WebResponse response, string FileName)
        {
            bool Value = true;
            byte[] buffer = new byte[1024];

            try
            {
                if (File.Exists(FileName))
                    File.Delete(FileName);
                Stream outStream = System.IO.File.Create(FileName);
                Stream inStream = response.GetResponseStream();

                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                }
                while (l > 0);

                outStream.Close();
                inStream.Close();
            }
            catch
            {
                Value = false;
            }
            return Value;
        }

    }
}
