using System;
using PetaPoco;

namespace YKCD.Agency.Business.EntityBase
{
    [Serializable]
    [TableName("Trackings")]
    [PrimaryKey("TrackingID", AutoIncrement = true)]
    public class TrackingBase
    {
        public long TrackingID { get; set; }
        public long RequestID { get; set; }
        public int UserID { get; set; }
    }
}
