﻿using System;
using PetaPoco;

namespace YKCD.SubAgency.Business.EntityBase
{
    [Serializable]
    [TableName("ReportFiles")]
    [PrimaryKey("ReportFileID", AutoIncrement = true)]
    public class ReportFileBase
    {
        public long ReportFileID { get; set; }
        public long FileID { get; set; }
        public long ReportID { get; set; }
    }
}
