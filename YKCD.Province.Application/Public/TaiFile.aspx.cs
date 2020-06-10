using System;
using System.IO;
using Framework.Helper;
using YKCD.Province.Application.Helper;

namespace YKCD.Province.Application.Public
{
    public partial class TaiFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileName = Parameters.FileName;

            if (File.Exists($@"{AppSettings.UploadFolder}\{fileName}"))
            {
                var fileReturn = File.Open($@"{AppSettings.UploadFolder}\{fileName}", FileMode.Open);
                byte[] bytBytes = new byte[fileReturn.Length];
                fileReturn.Read(bytBytes, 0, (int)fileReturn.Length);
                fileReturn.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + fileName.ToUnsignFileName());
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);
                Response.End();
            }
            else
            {
                Response.Redirect("FileNotFound.aspx");
            }
        }
    }
}