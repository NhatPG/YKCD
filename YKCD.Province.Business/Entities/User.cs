using System;
using PetaPoco;
using YKCD.Province.Business.EntityBase;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Business.Entities
{
    [Serializable]
    public class User : UserBase
    {
        [Ignore]
        public Department Department => DepartmentServices.GetById(DepartmentID);

        [Ignore]
        public string DepartmentName => Department.DepartmentName;
    }
}
