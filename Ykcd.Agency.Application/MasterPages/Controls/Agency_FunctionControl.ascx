<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Agency_FunctionControl.ascx.cs" Inherits="YKCD.Agency.Application.MasterPages.Controls.Agency_FunctionControl" %>
<div class="navbar-default sidebar" role="navigation">
    <div class="sidebar-nav navbar-collapse">
        <ul class="nav in" id="side-menu">
            <li style="<%= Sessions.UserID <= 0 ? "display : none": ""%>">
                <a><span class="glyphicon glyphicon-book" aria-hidden="true"></span><strong>VĂN BẢN</strong></a>

            </li>
            <li style="padding-left: 10px; <%= Sessions.UserID <= 0 ? "display : none": ""%>">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.VanBan_QuanLy) %>">
                    <span class="glyphicon glyphicon-plus-sign"></span>Nhập văn bản mới
                </a>
            </li>
            <li style="padding-left: 10px; <%= Sessions.UserID <= 0 ? "display : none": ""%>">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.VanBan_DanhSach) %>"><span class="glyphicon glyphicon-list"></span>Danh sách văn bản
                </a>
            </li>
            <li style="padding-left: 10px; <%= Sessions.UserID <= 0 ? "display : none": ""%>">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.VanBan_DongBo) %>"><span class="glyphicon glyphicon-random"></span>Đồng bộ văn bản
                </a>
            </li>
            <li>
                <a href="#">
                    <i class="glyphicon glyphicon-tasks" aria-hidden="true"></i><strong>TÌNH HÌNH THỰC HIỆN YKCD</strong></a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.DanhSachYKCD, "status", TrangThai.ChuaThucHien) %>">
                    <span class="glyphicon glyphicon-warning-sign"></span>Chưa thực hiện
                    <span class="label label-danger" style="float: right">
                        <asp:Literal ID="SoChuaThucHien" runat="server"></asp:Literal></span>
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.DanhSachYKCD, "status", TrangThai.DangThucHien) %>">
                    <span class="glyphicon glyphicon-road"></span>Đang thực hiện
                    <span class="label label-info" style="float: right">
                        <asp:Literal ID="SoDangThucHien" runat="server"></asp:Literal></span>
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.DanhSachYKCD, "status", TrangThai.DaThucHien) %>">
                    <span class="glyphicon glyphicon-ok-sign"></span>Đã hoàn thành
                    <span class="label label-success" style="float: right">
                        <asp:Literal ID="SoDaHoanThanh" runat="server"></asp:Literal></span>
                </a>
            </li>
            <li>
                <a href="#">
                    <i class="glyphicon glyphicon-stats" aria-hidden="true"></i><strong>THỐNG KÊ</strong></a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.ThongKe_YkcdUbndTinh) %>">
                    <i class="glyphicon glyphicon-bookmark" aria-hidden="true"></i>Tổng hợp YKCD UBND tỉnh
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.ThongKe_TinhHinhGiaoViec) %>">
                    <i class="glyphicon glyphicon-share" aria-hidden="true"></i>Giao việc YKCD UBND tỉnh
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.ThongKe_TheoLanhDao) %>">
                    <i class="glyphicon glyphicon-user" aria-hidden="true"></i>Theo lãnh đạo giao việc
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.ThongKe_TheoDonVi) %>">
                    <span class="glyphicon glyphicon-lock"></span>Theo đơn vị thực hiện
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.ThongKe_TheoChuyenVien) %>">
                    <i class="glyphicon glyphicon-user" aria-hidden="true"></i>Theo chuyên viên theo dõi
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Agency.ThongKe_CapNhatVanBan) %>">
                    <span class="glyphicon glyphicon-ok-sign"></span>Tình hình cập nhật văn bản
                </a>
            </li>
        </ul>
    </div>
</div>
