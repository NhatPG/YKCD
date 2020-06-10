using System;
using PetaPoco;
using YKCD.Province.Business.EntityBase;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Business.Entities
{
    [Serializable]
    public class Agency : AgencyBase
    {
        [Ignore]
        public AgencyGroup AgencyGroup => AgencyGroupServices.GetById(AgencyGroupID);

        [Ignore]
        public string AgencyGroupName => AgencyGroup.AgencyGroupName;
    }
}
