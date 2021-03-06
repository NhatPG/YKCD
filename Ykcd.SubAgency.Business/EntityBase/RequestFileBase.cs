﻿using System;
using PetaPoco;

namespace YKCD.SubAgency.Business.EntityBase
{
    [Serializable]
    [TableName("RequestFiles")]
    [PrimaryKey("RequestFileID", AutoIncrement = true)]
    public class RequestFileBase
    {
        public long RequestFileID { get; set; }
        public long RequestID { get; set; }
        public long FileID { get; set; }
    }
}
