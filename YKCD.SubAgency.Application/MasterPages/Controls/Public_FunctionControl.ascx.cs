﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Helper;
using YKCD.SubAgency.Business.Services;

namespace YKCD.SubAgency.Application.MasterPages.Controls
{
    public partial class Public_FunctionControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DanhSachNhomDonVi.BindData(DepartmentGroupServices.GetList(isShowStatistic: true));
            DanhSachLoaiVanBan.BindData(DocumentCategoryServices.GetList());
        }
    }
}