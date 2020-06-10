using System;
using PetaPoco;
using YKCD.SubAgency.Business.EntityBase;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Business.Entities
{
    [Serializable]
    public class User : UserBase
    {
        [Ignore]
        public Department Department => DepartmentServices.GetById(DepartmentID);

        [Ignore]
        public string DepartmentName => Department?.DepartmentName;
    }
}
