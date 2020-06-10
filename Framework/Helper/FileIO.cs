using System;
using System.IO;
using System.Web;

namespace Framework.Helper
{
    public static class FileIO
    {
        public static string SaveFile(this object fileObj, string fileName, string path)
        {
            try
            {
                //Kiểm tra nếu tạo thư mục Upload thành công thì mới tiến hành lưu file
                if (CreateFolder(path) == true)
                {
                    if (fileObj == null || string.IsNullOrEmpty(fileName))
                        return string.Empty;

                    fileName = GenerateFileName(fileName, path);
                    string filePath = $"{path}\\{fileName}";

                    if (fileObj.GetType() == typeof(byte[]) && ((byte[])fileObj).LongLength > 0)
                    {
                        File.WriteAllBytes(filePath, (byte[])fileObj);
                    }

                    if (fileObj.GetType() == typeof(HttpPostedFile) && ((HttpPostedFile)fileObj).ContentLength > 0)
                    {
                        byte[] myData = new byte[((HttpPostedFile)fileObj).ContentLength];
                        ((HttpPostedFile)fileObj).InputStream.Read(myData, 0, ((HttpPostedFile)fileObj).ContentLength);
                        File.WriteAllBytes(filePath, myData);
                    }
                }
                
                return fileName;
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(ex);
                return string.Empty;
            }
        }

        public static bool CreateFolder(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                return false;
            }
        }

        public static string GenerateFileName(string fileName, string path)
        {
            int count = 1;
            fileName = fileName.ToUnsignFileName();

            string fwe = Path.GetFileNameWithoutExtension(fileName).ToUnsignFileName();

            if (fwe.Length > 25)
            {
                fwe = fwe.Substring(0, 25);
            }

            string ext = Path.GetExtension(fileName);

            fileName = $"{fwe}{ext}";

            while (CheckFileExist(fileName, path))
            {
                fileName = $"{fwe}_{count}{ext}";
                count++;
            }

            return fileName;
        }

        public static bool CheckFileExist(string fileName, string path)
        {
            return File.Exists($"{path}\\{fileName}");
        }

        public static int GetFileLength(string path)
        {
            FileStream objfilestream = new FileStream(path, FileMode.Open, FileAccess.Read);
            int len = (int)objfilestream.Length;
            objfilestream.Close();

            return len;
        }

        public static byte[] GetFileContent(string path)
        {

            FileStream objfilestream = new FileStream(path, FileMode.Open, FileAccess.Read);
            int len = (int)objfilestream.Length;
            byte[] documentcontents = new byte[len];
            objfilestream.Read(documentcontents, 0, len);
            objfilestream.Close();

            return documentcontents;
        }
    }
}