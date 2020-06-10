<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Province_FunctionControl.ascx.cs" Inherits="YKCD.Province.Application.MasterPages.Controls.Province_FunctionControl" %>
<div class="navbar-default sidebar" role="navigation">
    <div class="sidebar-nav navbar-collapse">
        <ul class="nav in" id="side-menu">
            <li style="<%= Sessions.AgencyID > 0 ? "display : none": ""%>">
                <a><span class="glyphicon glyphicon-book" aria-hidden="true"></span><strong>VĂN BẢN</strong></a>
            </li>
            <%--<li style="padding-left: 10px; <%= Sessions.AgencyID > 0 ? "display : none": ""%>">
                <a href="<%= Redirector.GetLink(ViewNames.Province.VanBan_QuanLy) %>">
                    <span class="glyphicon glyphicon-plus-sign"></span>Nhập văn bản mới
                </a>
            </li>--%>
            <li style="padding-left: 10px; <%= Sessions.AgencyID > 0 ? "display : none": ""%>">
                <a href="<%= Redirector.GetLink(ViewNames.Province.VanBan_DanhSach) %>"><span class="glyphicon glyphicon-list"></span>Danh sách văn bản
                </a>
            </li>
            <li style="padding-left: 10px; <%= Authenticator.CheckRole(UserRole.ChuyenVienVPUBNDTinh, UserRole.LanhDaoUBNDTinh, UserRole.LanhDaoVPUBNDTinh, UserRole.Administrator) && !string.IsNullOrEmpty(AppSettings.HSCV_Service) ? "": "display : none"%>">
                <a href="<%= Redirector.GetLink(ViewNames.Province.VanBan_DongBo) %>"><span class="glyphicon glyphicon-random"></span>Đồng bộ văn bản
                </a>
            </li>
            
            <li>
                <a href="#">
                    <i class="glyphicon glyphicon-tasks" aria-hidden="true"></i><strong>TÌNH HÌNH THỰC HIỆN YKCD</strong></a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachYKCD, "status", TrangThai.ChuaThucHien) %>">
                    <span class="glyphicon glyphicon-warning-sign"></span>Chưa thực hiện
                    <span class="label label-danger" style="float: right">
                        <asp:Literal ID="SoChuaThucHien" runat="server"></asp:Literal></span>
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachYKCD, "status", TrangThai.DangThucHien) %>">
                    <span class="glyphicon glyphicon-road"></span>Đang thực hiện
                    <span class="label label-info" style="float: right">
                        <asp:Literal ID="SoDangThucHien" runat="server"></asp:Literal></span>
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachYKCD, "status", TrangThai.DaThucHien) %>">
                    <span class="glyphicon glyphicon-ok-sign"></span>Đã hoàn thành
                    <span class="label label-success" style="float: right">
                        <asp:Literal ID="SoDaHoanThanh" runat="server"></asp:Literal></span>
                </a>
            </li>
            <li>
                <a href="#">
                    <i class="glyphicon glyphicon-stats" aria-hidden="true"></i><strong>THỐNG KÊ</strong></a>
            </li>
            <li style="padding-left: 10px <%= Authenticator.CheckRole(UserRole.LanhDaoVPUBNDTinh) && Sessions.User.UserName.Equals("dntran.ubnd") ? "": "display : none"%>">
                <a href="<%= Redirector.GetLink(ViewNames.Province.ThongKe_KinhTeNganhKntc) %>">
                    <span class="glyphicon glyphicon-ok-sign"></span>Lĩnh vực Kinh tế - KNTC
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Province.ThongKe_TheoLanhDao) %>">
                    <i class="glyphicon glyphicon-user" aria-hidden="true"></i>Theo người ký văn bản
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Province.ThongKe_TheoDonVi) %>">
                    <span class="glyphicon glyphicon-lock"></span>Theo đơn vị thực hiện
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Province.ThongKe_TheoChuyenVien) %>">
                    <i class="glyphicon glyphicon-user" aria-hidden="true"></i>Theo người theo dõi
                </a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Province.ThongKe_CapNhatVanBan) %>">
                    <span class="glyphicon glyphicon-ok-sign"></span>Tình hình cập nhật văn bản
                </a>
            </li>
            <li style="padding-left: 10px; <%= Sessions.AgencyID > 0 ? "display : none": ""%>">
                <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachCuocHop) %>"><span class="glyphicon glyphicon-calendar"></span>Danh sách cuộc họp
                </a>
            </li>
        </ul>
    </div>
</div>
