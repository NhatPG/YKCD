using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Application.HscvService;
using YKCD.Province.Business.Components;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Province
{
    public partial class VanBan_ThongBaoKetLuan : ManagePageBase
    {
        private Document item;
        public string VBDiCode;
        protected override void SetBaseControl()
        {
            LongId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "NHẬP THÔNG TIN THÔNG BÁO KẾT LUẬN";
            UpdateTitle = "NHẬP THÔNG TIN THÔNG BÁO KẾT LUẬN";

            btnSave.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSave, null) + ";");
        }

        protected override void BindValueToPageControls()
        {
            foreach (var doc in DocumentServices.GetList(userId: Sessions.UserID).OrderByDescending(item => item.ReleaseDate))
            {
                VanBan.AddSelectItem($"{doc.DocumentCode} - {doc.ReleaseDate.ToDateString()} - {doc.MainContent}", doc.DocumentID.ToString());
            }
        }

        protected override void SetDefaultValueOnCreate()
        {

        }

        protected void ShowVBDHDocumentInfo()
        {

        }

        protected override void CreateNewObject()
        {
            
        }

        protected override void UpdateObject()
        {
            Metting metting = MettingServices.GetById(IntId);

            if (metting != null)
            {
                metting.DocumentID = VanBan.SelectedValue.ToInteger();
                MettingServices.Update(metting);
            }

            Redirector.Redirect(ViewNames.Province.DanhSachCuocHop);
        }
    }
}