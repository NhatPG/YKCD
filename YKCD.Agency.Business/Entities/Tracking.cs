using System;
using PetaPoco;
using YKCD.Agency.Business.EntityBase;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Business.Entities
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
