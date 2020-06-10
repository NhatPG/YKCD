<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SubAgency.Master" AutoEventWireup="true" CodeBehind="BangThongKe.aspx.cs" Inherits="YKCD.SubAgency.Application.SubAgency.BangThongKe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>TỔNG HỢP TÌNH HÌNH THỰC HIỆN Ý KIẾN CHỈ ĐẠO<br />
            <small>
                <asp:Literal ID="SmallHeader" runat="server"></asp:Literal>
            </small>
        </legend>
    </fieldset>
    <table class="table table-centered table-bordered table-striped table-hover table-data" id="mainContentHolder_grdStatistic">
        <tbody>
            <tr>
                <th rowspan="2"></th>
                <th colspan="2">Chưa thực hiện</th>
                <th colspan="2">Đang thực hiện</th>
                <th colspan="3">Đã hoàn thành</th>
            </tr>
            <tr>
                <th scope="col">Chưa đến hạn</th>
                <th scope="col">Đã quá hạn</th>
                <th scope="col">Chưa đến hạn</th>
                <th scope="col">Đã quá hạn</th>
                <th scope="col">Chờ xác nhận</th>
                <th scope="col">Đúng hạn</th>
                <th scope="col">Quá hạn</th>
            </tr>
            <asp:Repeater ID="UbTinhDetail" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="text-center">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.YkcdUbndTinh, true) %>">
                                <strong>YKCD của <%= AppSettings.AGENCY_NAME %></strong>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.YkcdUbndTinh, true, "status", TrangThai.ChuaThucHienTrongHan) %>">
                                <%# Eval("NotPerformInTerm").ToInteger() > 0 ? Eval("NotPerformInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.YkcdUbndTinh, true, "status", TrangThai.ChuaThucHienQuaHan) %>">
                                <%# Eval("NotPerformOutTerm").ToInteger() > 0 ? Eval("NotPerformOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.YkcdUbndTinh, true, "status", TrangThai.DangThucHienTrongHan) %>">
                                <%# Eval("PerformingInTerm").ToInteger() > 0 ? Eval("PerformingInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.YkcdUbndTinh, true, "status", TrangThai.DangThucHienQuaHan) %>">
                                <%# Eval("PerformingOutTerm").ToInteger() > 0 ? Eval("PerformingOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.YkcdUbndTinh, true, "status", TrangThai.ChoXacNhan) %>">
                                <%# Eval("WaitToConfirm").ToInteger() > 0 ? Eval("WaitToConfirm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.YkcdUbndTinh, true, "status", TrangThai.DaThucHienDungHan) %>">
                                <%# Eval("DoneInTerm").ToInteger() > 0 ? Eval("DoneInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.YkcdUbndTinh, true, "status", TrangThai.DaThucHienTreHan) %>">
                                <%# Eval("DoneOutTerm").ToInteger() > 0 ? Eval("DoneOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="TheoDoiDetail" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="text-center"><strong>Theo dõi</strong></td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Sessions.UserID, "status", TrangThai.ChuaThucHienTrongHan) %>">
                                <%# Eval("NotPerformInTerm").ToInteger() > 0 ? Eval("NotPerformInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Sessions.UserID, "status", TrangThai.ChuaThucHienQuaHan) %>">
                                <%# Eval("NotPerformOutTerm").ToInteger() > 0 ? Eval("NotPerformOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Sessions.UserID, "status", TrangThai.DangThucHienTrongHan) %>">
                                <%# Eval("PerformingInTerm").ToInteger() > 0 ? Eval("PerformingInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Sessions.UserID, "status", TrangThai.DangThucHienQuaHan) %>">
                                <%# Eval("PerformingOutTerm").ToInteger() > 0 ? Eval("PerformingOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Sessions.UserID, "status", TrangThai.ChoXacNhan) %>">
                                <%# Eval("WaitToConfirm").ToInteger() > 0 ? Eval("WaitToConfirm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Sessions.UserID, "status", TrangThai.DaThucHienDungHan) %>">
                                <%# Eval("DoneInTerm").ToInteger() > 0 ? Eval("DoneInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Sessions.UserID, "status", TrangThai.DaThucHienTreHan) %>">
                                <%# Eval("DoneOutTerm").ToInteger() > 0 ? Eval("DoneOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="GiaoViecDetail" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="text-center"><strong>Giao việc</strong></td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.PresidentID, Sessions.UserID, "status", TrangThai.ChuaThucHienTrongHan) %>">
                                <%# Eval("NotPerformInTerm").ToInteger() > 0 ? Eval("NotPerformInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.PresidentID, Sessions.UserID, "status", TrangThai.ChuaThucHienQuaHan) %>">
                                <%# Eval("NotPerformOutTerm").ToInteger() > 0 ? Eval("NotPerformOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.PresidentID, Sessions.UserID, "status", TrangThai.DangThucHienTrongHan) %>">
                                <%# Eval("PerformingInTerm").ToInteger() > 0 ? Eval("PerformingInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.PresidentID, Sessions.UserID, "status", TrangThai.DangThucHienQuaHan) %>">
                                <%# Eval("PerformingOutTerm").ToInteger() > 0 ? Eval("PerformingOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.PresidentID, Sessions.UserID, "status", TrangThai.ChoXacNhan) %>">
                                <%# Eval("WaitToConfirm").ToInteger() > 0 ? Eval("WaitToConfirm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.PresidentID, Sessions.UserID, "status", TrangThai.DaThucHienDungHan) %>">
                                <%# Eval("DoneInTerm").ToInteger() > 0 ? Eval("DoneInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.PresidentID, Sessions.UserID, "status", TrangThai.DaThucHienTreHan) %>">
                                <%# Eval("DoneOutTerm").ToInteger() > 0 ? Eval("DoneOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="DonViThucHienDetail" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="text-center"><strong>Đơn vị thực hiện</strong></td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.DepartmentID, Sessions.DepartmentID, "status", TrangThai.ChuaThucHienTrongHan) %>">
                                <%# Eval("NotPerformInTerm").ToInteger() > 0 ? Eval("NotPerformInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.DepartmentID, Sessions.DepartmentID, "status", TrangThai.ChuaThucHienQuaHan) %>">
                                <%# Eval("NotPerformOutTerm").ToInteger() > 0 ? Eval("NotPerformOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.DepartmentID, Sessions.DepartmentID, "status", TrangThai.DangThucHienTrongHan) %>">
                                <%# Eval("PerformingInTerm").ToInteger() > 0 ? Eval("PerformingInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.DepartmentID, Sessions.DepartmentID, "status", TrangThai.DangThucHienQuaHan) %>">
                                <%# Eval("PerformingOutTerm").ToInteger() > 0 ? Eval("PerformingOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.DepartmentID, Sessions.DepartmentID > 0, "status", TrangThai.ChoXacNhan) %>">
                                <%# Eval("WaitToConfirm").ToInteger() > 0 ? Eval("WaitToConfirm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.DepartmentID, Sessions.DepartmentID, "status", TrangThai.DaThucHienDungHan) %>">
                                <%# Eval("DoneInTerm").ToInteger() > 0 ? Eval("DoneInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.DepartmentID, Sessions.DepartmentID, "status", TrangThai.DaThucHienTreHan) %>">
                                <%# Eval("DoneOutTerm").ToInteger() > 0 ? Eval("DoneOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="NguoiThucHienDetail" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="text-center"><strong>Cá nhân thực hiện</strong></td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.UserID, Sessions.UserID, "status", TrangThai.ChuaThucHienTrongHan) %>">
                                <%# Eval("NotPerformInTerm").ToInteger() > 0 ? Eval("NotPerformInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.UserID, Sessions.UserID, "status", TrangThai.ChuaThucHienQuaHan) %>">
                                <%# Eval("NotPerformOutTerm").ToInteger() > 0 ? Eval("NotPerformOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.UserID, Sessions.UserID, "status", TrangThai.DangThucHienTrongHan) %>">
                                <%# Eval("PerformingInTerm").ToInteger() > 0 ? Eval("PerformingInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.UserID, Sessions.UserID, "status", TrangThai.DangThucHienQuaHan) %>">
                                <%# Eval("PerformingOutTerm").ToInteger() > 0 ? Eval("PerformingOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.UserID, Sessions.UserID > 0, "status", TrangThai.ChoXacNhan) %>">
                                <%# Eval("WaitToConfirm").ToInteger() > 0 ? Eval("WaitToConfirm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.UserID, Sessions.UserID, "status", TrangThai.DaThucHienDungHan) %>">
                                <%# Eval("DoneInTerm").ToInteger() > 0 ? Eval("DoneInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.UserID, Sessions.UserID, "status", TrangThai.DaThucHienTreHan) %>">
                                <%# Eval("DoneOutTerm").ToInteger() > 0 ? Eval("DoneOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Content>
