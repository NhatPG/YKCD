using PetaPoco;
using System;

namespace YKCD.Province.Business.Entities
{
    [Serializable]
    [TableName("ActivityLogs")]
    [PrimaryKey("ActivityLogID", AutoIncrement = true)]
    public class ActivityLog
    {
        public long ActivityLogID { get; set; }
        public long? RequestID { get; set; }
        public string Comment { get; set; }
        public string Requester { get; set; }
        public string Performer { get; set; }
    }
}
