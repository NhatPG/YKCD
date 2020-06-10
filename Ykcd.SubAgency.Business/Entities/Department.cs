using System;
using PetaPoco;
using YKCD.SubAgency.Business.EntityBase;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Business.Entities
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
