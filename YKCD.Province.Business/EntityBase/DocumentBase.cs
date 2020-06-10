using System;
using PetaPoco;

namespace YKCD.Province.Business.EntityBase
{
    [Serializable]
    [TableName("Documents")]
    [PrimaryKey("DocumentID", AutoIncrement = true)]
    public class DocumentBase
    {
        public long DocumentID { get; set; }
        public int DocumentCategoryID { get; set; }
        public string DocumentCode { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int SignerID { get; set; }
        public string SignerName { get; set; }
        public int WriterID { get; set; }
        public string MainContent { get; set; }
        public int Status { get; set; } = 0;
        public string Note { get; set; }
        public int TotalLateDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string VBDHCode { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public long? Ykcdv4ID { get; set; }
    }
}