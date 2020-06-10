using System;
using PetaPoco;

namespace YKCD.SubAgency.Business.EntityBase
{
    [Serializable]
    [TableName("Files")]
    [PrimaryKey("FileID", AutoIncrement = true)]
    public class FileBase
    {
        public long FileID { get; set; }
        public string FileName { get; set; }
    }
}
