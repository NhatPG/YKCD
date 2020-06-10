using System;
using Framework.Helper;

namespace YKCD.SubAgency.Application.Helper
{
    public class Parameters
    {
        public static int Id => ParameterNames.Id.ToUrlInteger();
        public static int DepartmentID => ParameterNames.DepartmentID.ToUrlInteger();
        public static int UserID => ParameterNames.UserID.ToUrlInteger();
        public static int PresidentID => ParameterNames.PresidentID.ToUrlInteger();
        public static int StaffID => ParameterNames.StaffID.ToUrlInteger();
        public static string Status => ParameterNames.Status.ToUrlString();
        public static int Pid => ParameterNames.Pid.ToUrlInteger();
        public static int CategoryId => ParameterNames.CategoryId.ToUrlInteger();
        public static string FileName => ParameterNames.FileName.ToUrlString();
        public static string FileType => ParameterNames.FileType.ToUrlString();
        public static string Year => ParameterNames.Year.ToUrlString();
        public static string VbdhCode => ParameterNames.VbdhCode.ToUrlString();
        public static DateTime? FromDate => ParameterNames.FromDate.ToUrlDate();

        public static bool YkcdUbndTinh => !string.IsNullOrEmpty(ParameterNames.YkcdUbndTinh.ToUrlString());
    }

    public static class ParameterNames
    {
        public const string Id = "id";
        public const string DepartmentID = "did";
        public const string PresidentID = "pid";
        public const string LeaderID = "lid";
        public const string StaffID = "sid";
        public const string UserID = "uid";
        public const string Status = "status";
        public const string Pid = "pid";
        public const string CategoryId = "cid";
        public const string FileName = "fname";
        public const string FileType = "loaifile";
        public const string Year = "nam";
        public const string VbdhCode = "vbdhcode";
        public const string FromDate = "fromdate";
        public const string YkcdUbndTinh = "ubtinh";
    }
}