using System;
using PetaPoco;

namespace YKCD.Province.Business.EntityBase
{
    [Serializable]
    [TableName("AgencyGroups")]
    [PrimaryKey("AgencyGroupID", AutoIncrement = true)]
    public class AgencyGroupBase
    {
        public int AgencyGroupID { get; set; }
        public string AgencyGroupName { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsShowStatistic { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}