using System;
using PetaPoco;

namespace YKCD.Agency.Business.Entities
{
    [Serializable]
    [TableName("Sms")]
    [PrimaryKey("SmsId", AutoIncrement = true)]
    public class Sms
    {
        public long SmsId { get; set; }
        public string SmsContent { get; set; }
        public string ReceiverNumber { get; set; }
        public DateTime? SendTime { get; set; }
    }
}
