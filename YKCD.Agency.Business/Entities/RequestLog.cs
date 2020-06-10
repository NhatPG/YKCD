using System;

namespace YKCD.Agency.Business.Entities
{
    public class RequestLog
    {
        public long RequestLogID { get; set; }
        public long? RequestID { get; set; }
        public string LogContent { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
