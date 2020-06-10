using System;
using PetaPoco;

namespace YKCD.SubAgency.Business.EntityBase
{
    [Serializable]
    [TableName("Reports")]
    [PrimaryKey("ReportID", AutoIncrement = true)]
    public class ReportBase
    {
        public long ReportID { get; set; }
        public long? PerformID { get; set; }
        public long RequestID { get; set; }
        public int Status { get; set; }
        public string ReportContent { get; set; }
        public DateTime PerformOnDate { get; set; }
        public string ReporterName { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? DepartmentID { get; set; } = 0;
        public int? CreatedBy { get; set; } = 0;
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public bool IsTransferred { get; set; }
        public long? Ykcdv4ID { get; set; }
    }
}
