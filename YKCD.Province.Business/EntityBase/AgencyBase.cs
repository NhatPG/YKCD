using System;
using PetaPoco;

namespace YKCD.Province.Business.EntityBase
{
    [Serializable]
    [TableName("Agencies")]
    [PrimaryKey("AgencyID", AutoIncrement = true)]
    public class AgencyBase
    {
        public int AgencyID { get; set; }
        public int AgencyGroupID { get; set; }
        public string AgencyName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsShowStatistic { get; set; }
        public bool? IsUseInternalSoftware { get; set; }
        public string ServiceUrl { get; set; }
        public bool? IsDeleted { get; set; }
        public string PhoneNumber { get; set; }
        public string SecondPhoneNumber { get; set; }
        public string AgencyIdentifierCode { get; set; }
        public string AgencyDomain { get; set; }
    }
}