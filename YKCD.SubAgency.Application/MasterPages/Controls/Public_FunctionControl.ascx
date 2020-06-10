<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Public_FunctionControl.ascx.cs" Inherits="YKCD.SubAgency.Application.MasterPages.Controls.Public_FunctionControl" %>
<div class="navbar-default sidebar" role="navigation">
    <div class="sidebar-nav navbar-collapse">
        <ul class="nav in" id="side-menu">
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Public.ThongKe_NguoiKyVanBan) %>">
                    <i class="glyphicon glyphicon-user" aria-hidden="true"></i><strong>NGƯỜI KÝ VĂN BẢN</strong></a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Public.ThongKe_NhomDonVi) %>">
                    <i class="glyphicon glyphicon-lock" aria-hidden="true"></i><strong>ĐƠN VỊ THỰC HIỆN</strong></a>
            </li>
            <asp:Repeater ID="DanhSachNhomDonVi" runat="server">
                <ItemTemplate>
                    <li>
                        <a href="<%# Redirector.GetLink(ViewNames.Public.ThongKe_DonVi, "id", Eval("DepartmentGroupID")) %>">
                            <%# Eval("DepartmentGroupName") %></a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Public.ThongKe_NguoiTheoDoi) %>">
                    <i class="glyphicon glyphicon-user" aria-hidden="true"></i><strong>NGƯỜI THEO DÕI</strong></a>
            </li>
            <li style="padding-left: 10px">
                <a href="<%= Redirector.GetLink(ViewNames.Public.VanBan_DanhSach) %>">
                    <i class="glyphicon glyphicon-book" aria-hidden="true"></i><strong>VĂN BẢN CHỈ ĐẠO</strong></a>
            </li>
            <asp:Repeater ID="DanhSachLoaiVanBan" runat="server">
                <ItemTemplate>
                    <li>
                        <a href="<%# Redirector.GetLink(ViewNames.Public.VanBan_DanhSach, "id", Eval("DocumentCategoryID")) %>">
                            <%# Eval("DocumentCategoryName") %></a>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>
