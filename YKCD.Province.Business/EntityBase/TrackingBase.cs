using System;
using PetaPoco;

namespace YKCD.Province.Business.EntityBase
{
    /// <summary>
    /// Thông tin chuyên viên theo dõi YKCD
    /// </summary>
    [Serializable]
    [TableName("Trackings")]
    [PrimaryKey("TrackingID", AutoIncrement = true)]
    public class TrackingBase
    {
        /// <summary>
        /// Mã theo dõi
        /// </summary>
        public long TrackingID { get; set; }

        /// <summary>
        /// Mã YKCD
        /// </summary>
        public long RequestID { get; set; }

        /// <summary>
        /// Mã thực hiện YKCD
        /// </summary>
        public long? PerformID { get; set; }

        /// <summary>
        /// Mã người theo dõi
        /// </summary>
        public int UserID { get; set; }
    }
}