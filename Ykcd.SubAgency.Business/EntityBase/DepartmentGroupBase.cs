using System;
using PetaPoco;

namespace YKCD.SubAgency.Business.EntityBase
{
    [Serializable]
    [TableName("DepartmentGroups")]
    [PrimaryKey("DepartmentGroupID", AutoIncrement = true)]
    public class DepartmentGroupBase
    {
        public int DepartmentGroupID { get; set; }
        public string DepartmentGroupName { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsShowStatistic { get; set; }
    }
}
