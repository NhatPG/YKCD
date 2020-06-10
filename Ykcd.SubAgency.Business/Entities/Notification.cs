using System;
using YKCD.SubAgency.Business.EntityBase;

namespace YKCD.SubAgency.Business.Entities
{
    [Serializable]
    public class Notification : NotificationBase
    {
        public Notification() { }
        public Notification(long requestID, int userID = 0, int departmentID = 0, string comment = "")
        {
            RequestID = requestID;
            UserID = userID;
            DepartmentID = departmentID;
            Comment = comment;
        }
    }
}
