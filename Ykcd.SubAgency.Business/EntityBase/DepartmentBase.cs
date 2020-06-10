using System;
using PetaPoco;

namespace YKCD.SubAgency.Business.EntityBase
{
    [Serializable]
    [TableName("Departments")]
    [PrimaryKey("DepartmentID", AutoIncrement = true)]
    public class DepartmentBase
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsShowStatistic { get; set; }
        public int DepartmentGroupID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
