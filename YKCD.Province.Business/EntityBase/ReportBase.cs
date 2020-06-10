using System;
using PetaPoco;

namespace YKCD.Province.Business.EntityBase
{
    [Serializable]
    [TableName("Reports")]
    [PrimaryKey("ReportID", AutoIncrement = true)]
    public class ReportBase
    {
        public long ReportID { get; set; }
        public long? AgencyReportID { get; set; }
        public long RequestID { get; set; }
        public int Status { get; set; }
        public string ReportContent { get; set; }
        public DateTime PerformOnDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int AgencyID { get; set; }
        public long? Ykcdv4ID { get; set; }
    }
}