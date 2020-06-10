using PetaPoco;
using System;
using YKCD.Province.Business.EntityBase;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Business.Entities
{
    [Serializable]
    public class Perform : PerformBase
    {
        public Perform() { }

        public Perform(long requestID, int agencyID, DateTime requiredDate)
        {
            RequestID = requestID;
            AgencyID = agencyID;
            RequiredDate = requiredDate;
            Status = 0;
        }

        [Ignore]
        public Agency Agency => AgencyServices.GetById(AgencyID);

        [Ignore]
        public Request Request => RequestServices.GetById(RequestID);

        [Ignore]
        public string AgencyName => Agency.AgencyName;

        [Ignore]
        public string StatusString
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "<tag class=\"text-danger\">Chưa thực hiện</tag>";
                    case 1:
                        return "<tag class=\"text-info\">Đang thực hiện</tag>";
                    case 2:
                        return "<tag class=\"text-success\">Đã hoàn thành</tag></tag>";
                    case 3:
                        return "<tag class=\"text-warning\">Chờ xác nhận</tag>";
                }
                return string.Empty;
            }
        }
    }
}
