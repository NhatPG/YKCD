using System;
using System.Collections.Generic;
using PetaPoco;
using YKCD.Agency.Business.EntityBase;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Business.Entities
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
