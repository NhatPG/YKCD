using System;
using PetaPoco;

namespace YKCD.Province.Business.Entities
{
    [Serializable]
    [TableName("ResponseFiles")]
    [PrimaryKey("ResponseFileID", AutoIncrement = true)]
    public class ResponseFile
    {
        public long ResponseFileID { get; set; }
        public long? ResponseID { get; set; }
        public long? FileID { get; set; }
    }
}
