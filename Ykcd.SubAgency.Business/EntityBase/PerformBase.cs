using System;
using PetaPoco;

namespace YKCD.SubAgency.Business.EntityBase
{
    [Serializable]
    [TableName("Performs")]
    [PrimaryKey("PerformID", AutoIncrement = true)]
    public class PerformBase
    {
        public long PerformID { get; set; }
        public long RequestID { get; set; }
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public bool IsMainPerform { get; set; }
        public int Status { get; set; } = 0;
        public DateTime FinishedOnDate { get; set; } = new DateTime(1990, 1, 1);
        public DateTime RequiredDate { get; set; } = new DateTime(1990, 1, 1);
        public bool IsFinishedConfirm { get; set; }
        public bool IsAssigned { get; set; } = false;
        public long ParentPerformID { get; set; } = 0;
        public long CreatedBy { get; set; } = 0;
        public DateTime? CreatedTime { get; set; } = DateTime.Now;

    }
}
