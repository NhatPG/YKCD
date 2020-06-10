using System;
using System.Collections.Generic;
using PetaPoco;
using YKCD.SubAgency.Business.EntityBase;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Business.Entities
{
    [Serializable]
    public class Report : ReportBase
    {
        [Ignore]
        public Request Request => RequestServices.GetById(RequestID);

        [Ignore]
        public List<File> Files => FileServices.GetList(reportId: ReportID);

        [Ignore]
        public string LinkFiles => Files.ShowFiles();
    }
}
