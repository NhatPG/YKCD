using System;
using YKCD.Province.Business.EntityBase;

namespace YKCD.Province.Business.Entities
{
    [Serializable]
    public class Notification : NotificationBase
    {
        public Notification()
        { }

        public Notification(long requestID, int userID, int agencyID, string comment)
        {
            RequestID = requestID;
            UserID = userID;
            AgencyID = agencyID;
            Comment = comment;
        }
    }
}
