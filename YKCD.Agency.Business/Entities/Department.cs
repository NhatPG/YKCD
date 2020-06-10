using System;
using PetaPoco;
using YKCD.Agency.Business.EntityBase;
using YKCD.Agency.Business.Services;

namespace YKCD.Agency.Business.Entities
{
    [Serializable]
    public class Department : DepartmentBase
    {
        /// <summary>
        /// Thông tin nhóm đơn vị
        /// </summary>
        [Ignore]
        public DepartmentGroup DepartmentGroup => DepartmentGroupServices.GetById(DepartmentGroupID);

        /// <summary>
        /// Tên nhóm đơn vị
        /// </summary>
        [Ignore]
        public string DepartmentGroupName => DepartmentGroup.DepartmentGroupName;
    }
}
