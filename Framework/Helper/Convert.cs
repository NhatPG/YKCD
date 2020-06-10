using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Framework.Helper
{
    public static class Convert
    {
        public static bool IsNullOrEmpty(this object obj)
        {
            return string.IsNullOrEmpty(obj?.ToString());
        }

        public static int ToInteger(this object obj)
        {
            try
            {
                return System.Convert.ToInt32(obj);
            }
            catch
            {
                return -1;
            }
        }

        public static long ToLong(this object obj)
        {
            try
            {
                return System.Convert.ToInt64(obj);
            }
            catch
            {
                return -1;
            }
        }

        public static short ToShort(this object obj)
        {
            try
            {
                return System.Convert.ToInt16(obj);
            }
            catch
            {
                return -1;
            }
        }

        public static bool ToBoolean(this object obj)
        {
            try
            {
                return System.Convert.ToBoolean(obj);
            }
            catch
            {
                return false;
            }
        }

        public static DateTime ToDateTime(this object obj)
        {
            if (obj != null)
            {
                if (obj is string)
                {
                    if (!string.IsNullOrEmpty(obj.ToString()))
                    {
                        try
                        {
                            return DateTime.ParseExact(obj.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            try
                            {
                                return DateTime.ParseExact(obj.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture);
                            }
                            catch
                            {
                                return new DateTime(1990, 1, 1);
                            }
                        }
                    }
                    return new DateTime(1990, 1, 1);
                }
                return System.Convert.ToDateTime(obj);
            }

            return new DateTime(1990, 1, 1);
        }

        public static DateTime? ToDateTimeNullable(this object obj)
        {
            if (obj != null)
            {
                if (obj is string)
                {
                    if (!string.IsNullOrEmpty(obj.ToString()))
                    {
                        try
                        {
                            return DateTime.ParseExact(obj.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        catch
                        {
                            try
                            {
                                return DateTime.ParseExact(obj.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture);
                            }
                            catch
                            {
                                return null;
                            }
                        }
                    }
                    return null;
                }

                return System.Convert.ToDateTime(obj);
            }

            return null;
        }

        public static string ToDateString(this object obj)
        {
            if (obj != null && obj.ToDateTime() != new DateTime(1990, 1, 1))
            {
                return obj.ToDateTime().ToString("dd/MM/yyyy");
            }

            return string.Empty;
        }

        public static string ToTimeString(this object obj)
        {
            if (obj != null && obj.ToDateTime() != new DateTime(1990, 1, 1))
            {
                return ToDateTime(obj).ToString("hh:mm dd/MM/yyyy");
            }

            return string.Empty;
        }

        public static string ToShortTimeString(this object obj)
        {
            if (obj != null && obj.ToDateTime() != new DateTime(1990, 1, 1))
            {
                return ToDateTime(obj).ToString("hh:mm");
            }

            return string.Empty;
        }

        public static string ToDateTimeString(this object obj)
        {
            if (obj != null && obj.ToDateTime() != new DateTime(1990, 1, 1))
            {
                return ToDateTime(obj).ToString("hh:mm dd/MM/yyyy");
            }

            return string.Empty;
        }

        public static byte[] ToByteArray(this HttpPostedFile postedFile)
        {
            try
            {
                byte[] fileData;

                using (var binaryReader = new BinaryReader(postedFile.InputStream))
                {
                    fileData = binaryReader.ReadBytes(postedFile.ContentLength);
                }

                return fileData;
            }
            catch
            {
                // ignored
            }
            return null;
        }

        public static string RemoveSpecialCharracters(this string str)
        {
            if (str != null)
            {
                str = str.Replace("&", "-");
                str = str.Replace("$", "-");
                str = str.Replace("/", "-");
                str = str.Replace(",", "-");
                str = str.Replace(";", "-");
                str = str.Replace("+", "-");
                str = str.Replace("=", "-");
                str = str.Replace("(", "-");
                str = str.Replace(")", "-");
                str = str.Replace("%", "-");
                str = str.Replace("*", "-");
                str = str.Replace("^", "-");
                str = str.Replace("%", "-");
                str = str.Replace("<", "-");
                str = str.Replace(">", "-");
                str = str.Replace(":", "-");
                str = str.Replace("\"", "-");
            }

            return str;
        }

        public static string RemoveBreakLineCharacters(this string str)
        {
            if (str != null)
            {
                str = str.Replace("\n", " ");
                str = str.Replace("\r", " ");
                str = str.Replace("\t", " ");
                str = str.Replace("\r\n", " ");
            }
            
            return str;
        }

        public static string ToUnsign(this string str)
        {
            str = str?.Trim();

            string[] signs = { "aAeEoOuUiIdDyY", "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ", "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ", "ÚÙỤỦŨƯỨỪỰỬỮ",
                    "íìịỉĩ", "ÍÌỊỈĨ", "đ", "Đ", "ýỳỵỷỹ", "ÝỲỴỶỸ" };

            for (int i = 1; i < signs.Length; i++)
            {
                for (int j = 0; j < signs[i].Length; j++)
                {
                    str = str?.Replace(signs[i][j], signs[0][i - 1]);
                }
            }
            return str;
        }

        public static string ToUnsignFileName(this string str)
        {
            str = str.ToUnsign().RemoveSpecialCharracters().Replace(" ", "_");

            return str;
        }

        public static string GetUrlEmbed(string str)
        {
            str = str.ToUnsign();
            return str;
        }

        public static string ToRtf(this string obj)
        {
            if (obj == null)
                obj = string.Empty;
            string strReturn = string.Empty;
            string arrChar = obj;

            foreach (char ch in arrChar)
            {
                int intCharCode = ch;
                if (intCharCode > 128)
                {
                    strReturn = strReturn + "\\u" + intCharCode + "  ";
                }
                else
                {
                    strReturn = strReturn + ch;
                }
            }
            return strReturn;
        }

        public static List<T> ToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, System.Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static List<T> WrapInList<T>(this T t)
        {
            return new List<T> { t };
        }

        public static string ToShortString(this string str, int lenght)
        {
            if (str.Length > lenght)
                return str.Substring(0, lenght) + "...";
            return str;
        }

        public static int GetSessionInteger(string sessionKey)
        {
            if (HttpContext.Current.Session != null)
            {
                return HttpContext.Current.Session[sessionKey].ToInteger();
            }

            return 0;
        }

        /// <summary>
        /// Hiển thị danh sách chuỗi trên một dòng
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string DisplayInline(this List<string> obj)
        {
            string result = string.Empty;

            if (obj != null)
            {
                if (obj.Count == 1)
                    return $"{obj.First()} ";

                foreach (var name in obj)
                {
                    if (name.Equals(obj.Last()))
                        result += $"{name} ";
                    else
                        result += $"{name}, ";
                }
            }

            return result;
        }

        public static string DisplayInBreakLine(this List<string> obj)
        {
            string result = string.Empty;
            foreach (var name in obj)
            {
                result += $"{name}<br/>";
            }
            return result;
        }
    }

    public static class UrlConvert
    {
        public static string ToUrlString(this string paramName)
        {
            if (HttpContext.Current.Request.QueryString[paramName] != null)
                return HttpContext.Current.Request.QueryString[paramName];
            return string.Empty;
        }

        public static int ToUrlInteger(this string paramName)
        {
            return HttpContext.Current.Request.QueryString[paramName].ToInteger();
        }

        public static long ToUrlLong(this string paramName)
        {
            return HttpContext.Current.Request.QueryString[paramName].ToLong();
        }

        public static DateTime? ToUrlDate(this string paramName)
        {
            return HttpContext.Current.Request.QueryString[paramName]?.Replace("-", "/").ToDateTime();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }

            var collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count < 1;
            }
            return !enumerable.Any();
        }
    }
}