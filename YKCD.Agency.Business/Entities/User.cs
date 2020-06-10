using System;
using PetaPoco;
using YKCD.Agency.Business.EntityBase;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Business.Entities
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
