using System;
using PetaPoco;

namespace YKCD.Agency.Business.EntityBase
{
    [Serializable]
    [TableName("Requests")]
    [PrimaryKey("RequestID", AutoIncrement = true)]
    public class RequestBase
    {
        public long RequestID { get; set; }
        public long ProvinceRequestID { get; set; } = 0;
        public long ProvincePerformID { get; set; } = 0;
        public long ParentRequestID { get; set; } = 0;
        public long DocumentID { get; set; } = 0;
        public string RequestContent { get; set; }
        public DateTime RequiredDate { get; set; }
        public int RequesterID { get; set; }
        public string RequesterName { get; set; }
        public int Status { get; set; }
        public DateTime FinishedOnDate { get; set; } = new DateTime(1990, 1, 1);
        public bool IsDeleted { get; set; }
        public bool IsProvinceRequest { get; set; }
        public bool? IsAllowDirectReport { get; set; }
        public bool? IsHaveChildRequest { get; set; }
        public bool IsAssignPerform { get; set; }
        public bool? IsAssignTrack { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public bool? IsProcessing { get; set; }
        public long? Ykcdv4ID { get; set; }
    }
}
