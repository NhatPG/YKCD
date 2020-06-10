using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Framework.Helper;
using PetaPoco;
using YKCD.Province.Business.EntityBase;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Business.Entities
{
    [Serializable]
    public class Metting : MettingBase
    {
        [Ignore]
        public string TenNguoiDangKy => UserServices.GetById(StaffID)?.FullName;

        [Ignore]
        public string TenNguoiChuTri => UserServices.GetById(PresidentID)?.FullName;

        [Ignore]
        public Document Document => DocumentServices.GetById(DocumentID);

        [Ignore]
        public string DocumentCode => DocumentServices.GetById(DocumentID)?.DocumentCode;

        [Ignore]
        public string ReleasedDate => DocumentServices.GetById(DocumentID)?.ReleaseDate.ToDateString();
    }
}