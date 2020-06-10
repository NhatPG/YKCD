using System;
using PetaPoco;

namespace YKCD.Province.Business.EntityBase
{
    [Serializable]
    [TableName("Requests")]
    [PrimaryKey("RequestID", AutoIncrement = true)]
    public class RequestBase
    {
        public long RequestID { get; set; }
        public long DocumentID { get; set; }
        public string RequestContent { get; set; }
        public DateTime RequiredDate { get; set; }
        public int RequesterID { get; set; }
        public int Status { get; set; }
        public string RequesterName { get; set; }
        public DateTime FinishedOnDate { get; set; } = new DateTime(1990, 1, 1);
        public bool IsDeleted { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsProvinceRequest { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public long? Ykcdv4ID { get; set; }
    }
}