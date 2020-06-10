using Framework.Helper;
using Framework.Web;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Admin
{
    public partial class LoaiVanBan_QuanLy : ManagePageBase
    {
        private DocumentCategory item;

        protected override void SetBaseControl()
        {
            IntId = Parameters.Id;

            HeaderCaption = lblHeaderCaption;
            CreateTitle = "NHẬP THÔNG TIN LOẠI VĂN BẢN";
            UpdateTitle = "CẬP NHẬT THÔNG TIN LOẠI VĂN BẢN";
        }

        protected override void ShowObjectInformation()
        {
            item = DocumentCategoryServices.GetById(IntId);
            ViewState["CurrentObject"] = item;
            TenLoaiVanBan.Text = item.DocumentCategoryName;
            ThuTuHienThi.Text = item.DisplayOrder.ToString();
        }

        protected override void CreateNewObject()
        {
            item = new DocumentCategory();
            item.DocumentCategoryName = TenLoaiVanBan.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            DocumentCategoryServices.Create(item);
        }

        protected override void UpdateObject()
        {
            item = (DocumentCategory)ViewState["CurrentObject"];
            item.DocumentCategoryName = TenLoaiVanBan.Text.Trim();
            item.DisplayOrder = ThuTuHienThi.Text.ToInteger();
            DocumentCategoryServices.Update(item);
        }

        protected override void ActionAfterFinish()
        {
            Redirector.Redirect(ViewNames.Admin.LoaiVanBan_DanhSach);
        }
    }
}