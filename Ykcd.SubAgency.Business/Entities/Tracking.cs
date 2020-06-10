using System;
using PetaPoco;
using YKCD.SubAgency.Business.EntityBase;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Business.Entities
{
    [Serializable]
    public class Tracking : TrackingBase
    {
        public Tracking()
        { }

        public Tracking(long requestID, int userID)
        {
            RequestID = requestID;
            UserID = userID;
        }

        [Ignore]
        public User Tracker => UserServices.GetById(UserID);
    }
}
