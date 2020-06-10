using System;
using PetaPoco;

namespace YKCD.Province.Business.EntityBase
{
    [Serializable]
    [TableName("Notifications")]
    [PrimaryKey("NotificationID", AutoIncrement = true)]
    public class NotificationBase
    {
        public long NotificationID { get; set; }
        public long? RequestID { get; set; }
        public int? UserID { get; set; }
        public string Comment { get; set; }
        public bool? IsRead { get; set; } = false;
        public int? Type { get; set; }
        public DateTime? ReadTime { get; set; }
        public DateTime? CreatedTime { get; set; } = DateTime.Now;
        public int? AgencyID { get; set; }
    }
}