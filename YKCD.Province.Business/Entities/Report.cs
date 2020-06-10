using System;
using PetaPoco;
using System.Collections.Generic;
using YKCD.Province.Business.EntityBase;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Business.Entities
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
