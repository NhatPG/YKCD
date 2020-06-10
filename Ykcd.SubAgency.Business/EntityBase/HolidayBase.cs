using System;
using PetaPoco;

namespace YKCD.SubAgency.Business.EntityBase
{
    [Serializable]
    [TableName("Holidays")]
    [PrimaryKey("HolidayID", AutoIncrement = true)]
    public class HolidayBase
    {
        public int HolidayID { get; set; }
        public string HolidayName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
