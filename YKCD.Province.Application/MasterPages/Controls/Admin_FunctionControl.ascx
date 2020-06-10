<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin_FunctionControl.ascx.cs" Inherits="YKCD.Province.Application.MasterPages.Controls.Admin_FunctionControl" %>
<div class="navbar-default sidebar" role="navigation">
    <div class="sidebar-nav navbar-collapse">
        <ul class="nav in" id="side-menu">
            <li>
                <a><span class="glyphicon glyphicon-book" aria-hidden="true"></span><strong>DANH MỤC</strong></a>

            </li>
            <li style="padding-left: 10px;">
                <a href="<%= Redirector.GetLink(ViewNames.Admin.NhomDonVi_DanhSach) %>">
                    <span class="glyphicon glyphicon-lock"></span>Đơn vị
                </a>
            </li>
            <li style="padding-left: 10px;">
                <a href="<%= Redirector.GetLink(ViewNames.Admin.PhongBan_DanhSach) %>">
                    <span class="glyphicon glyphicon-bookmark"></span>Phòng ban nội bộ
                </a>
            </li>
            <li style="padding-left: 10px;">
                <a href="<%= Redirector.GetLink(ViewNames.Admin.NguoiSuDung_DanhSach) %>">
                    <span class="glyphicon glyphicon-user"></span>Người sử dụng
                </a>
            </li>
            <li style="padding-left: 10px;">
                <a href="<%= Redirector.GetLink(ViewNames.Admin.LoaiVanBan_DanhSach) %>">
                    <span class="glyphicon glyphicon-tag"></span>Loại văn bản
                </a>
            </li>
            <li style="padding-left: 10px;">
                <a href="<%= Redirector.GetLink(ViewNames.Admin.NgayNghi_DanhSach) %>">
                    <span class="glyphicon glyphicon-calendar"></span>Ngày nghỉ
                </a>
            </li>
        </ul>
    </div>
</div>
