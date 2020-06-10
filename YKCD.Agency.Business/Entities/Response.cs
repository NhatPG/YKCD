using System;
using PetaPoco;

namespace YKCD.Agency.Business.Entities
{
    [Serializable]
    [TableName("Responses")]
    [PrimaryKey("ResponseID", AutoIncrement = true)]
    public class Response
    {
        public long ResponseID { get; set; }
        public long ReportID { get; set; }
        public long RequestID { get; set; }
        public long? PerformID { get; set; }
        public string ResponseContent { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string ResponserName { get; set; }
        public int? Status { get; set; }
    }
}
