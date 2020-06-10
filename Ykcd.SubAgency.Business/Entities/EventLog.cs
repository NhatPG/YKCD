using System;

namespace YKCD.SubAgency.Business.Entities
{
    public class EventLog
    {
        public long EventLogID { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? CreatedBy { get; set; }
        public string IPAddress { get; set; }
        public string HostName { get; set; }
        public string LogContent { get; set; }
    }
}
