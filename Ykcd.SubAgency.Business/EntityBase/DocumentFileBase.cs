using System;
using PetaPoco;

namespace YKCD.SubAgency.Business.EntityBase
{
    [Serializable]
    [TableName("DocumentFiles")]
    [PrimaryKey("DocumentFileID", AutoIncrement = true)]
    public class DocumentFileBase
    {
        public long DocumentFileID { get; set; }
        public long DocumentID { get; set; }
        public long FileID { get; set; }
    }
}
