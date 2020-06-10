using System;
using PetaPoco;

namespace YKCD.Agency.Business.EntityBase
{
    [Serializable]
    [TableName("DocumentCategories")]
    [PrimaryKey("DocumentCategoryID", AutoIncrement = true)]
    public class DocumentCategoryBase
    {
        public int DocumentCategoryID { get; set; }
        public string DocumentCategoryName { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
