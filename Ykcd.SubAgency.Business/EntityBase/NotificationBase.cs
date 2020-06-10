using System;
using PetaPoco;

namespace YKCD.SubAgency.Business.EntityBase
{
    [Serializable]
    [TableName("Notifications")]
    [PrimaryKey("NotificationID", AutoIncrement = true)]
    public class NotificationBase
    {
        public long NotificationID { get; set; }
        public long? RequestID { get; set; }
        public int? DepartmentID { get; set; }
        public int? UserID { get; set; }
        public string Comment { get; set; }
        public bool? IsRead { get; set; } = false;
        public int? NotificationType { get; set; }
        public DateTime? ReadTime { get; set; }
        public DateTime? CreatedTime { get; set; }
        public bool? IsProvinceNotification { get; set; }
    }
}
