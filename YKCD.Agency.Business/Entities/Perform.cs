using System;
using PetaPoco;
using YKCD.Agency.Business.EntityBase;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Business.Entities
{
    /// <summary>
    /// Thông tin thực hiện ý kiến chỉ đạo
    /// </summary>
    [Serializable]
    public class Perform : PerformBase
    {
        public Perform() { }

        public Perform(long requestId, DateTime requiredDate, int departmentId = 0, int userId = 0)
        {
            RequestID = requestId;
            DepartmentID = departmentId;
            UserID = userId;
            RequiredDate = requiredDate;
            Status = 0;
        }

        /// <summary>
        /// Thông tin ý kiến chỉ đạo
        /// </summary>
        [Ignore]
        public Request Request => RequestServices.GetById(RequestID);

        [Ignore]
        public Department Department => DepartmentID > 0 ? DepartmentServices.GetById(DepartmentID) : new Department();

        [Ignore]
        public string DepartmentName => Department.DepartmentName;

        [Ignore]
        public User User => UserID > 0 ? UserServices.GetById(UserID) :  new User();

        [Ignore]
        public string StatusString
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "<tag class=\"text-danger\">Chưa thực hiện</tag>";
                    case 1:
                        return "<tag class=\"text-info\">Đang thực hiện</tag>";
                    case 2:
                        return "<tag class=\"text-success\">Đã hoàn thành</tag></tag>";
                    case 3:
                        return "<tag class=\"text-warning\">Chờ xác nhận</tag>";
                }
                return string.Empty;
            }
        }
    }
}
