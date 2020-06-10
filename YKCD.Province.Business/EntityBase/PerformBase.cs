using System;
using PetaPoco;

namespace YKCD.Province.Business.EntityBase
{
    [Serializable]
    [TableName("Performs")]
    [PrimaryKey("PerformID", AutoIncrement = true)]
    public class PerformBase
    {
        public long PerformID { get; set; }
        public long RequestID { get; set; }
        public int AgencyID { get; set; }
        public bool? IsMainPerform { get; set; }
        public int Status { get; set; } = 0;
        public DateTime FinishedOnDate { get; set; } = new DateTime(1990, 1, 1);
        public DateTime RequiredDate { get; set; }
        public bool? IsHaveReported { get; set; }
        public bool? IsSynced { get; set; } = false;
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}