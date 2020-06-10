using System;
using PetaPoco;
using YKCD.Province.Business.EntityBase;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Business.Entities
{
    [Serializable]
    public class Tracking : TrackingBase
    {
        public Tracking()
        {

        }

        public Tracking(long requestID, int userID)
        {
            RequestID = requestID;
            UserID = userID;
        }

        /// <summary>
        /// Thông tin người theo dõi
        /// </summary>
        [Ignore]
        public User Tracker => UserServices.GetById(UserID);
    }
}
