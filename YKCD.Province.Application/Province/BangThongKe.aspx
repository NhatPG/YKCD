<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Province.Master" AutoEventWireup="true" CodeBehind="BangThongKe.aspx.cs" Inherits="YKCD.Province.Application.Province.BangThongKe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>TỔNG HỢP TÌNH HÌNH BAN HÀNH VĂN BẢN THÔNG BÁO KẾT LUẬN CỦA LÃNH ĐẠO UBND TỈNH
        </legend>
    </fieldset>
    <table class="table table-centered table-bordered table-striped table-hover table-data">
        <tbody>
            <tr>
                <th colspan="2" style="background-color: #B22222 !important; color: #fff !important;">Chưa ban hành</th>
                <th colspan="2" style="background-color: #5CB85C !important; color: #fff !important;">Đã ban hành</th>
            </tr>
            <tr>
                <th scope="col">Trong hạn</th>
                <th scope="col">Quá hạn</th>
                <th scope="col">Trong hạn</th>
                <th scope="col">Quá hạn</th>
            </tr>
            <tr>
                <td class="text-in-term">
                    <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachCuocHop, "status", "ontime", "uid", Sessions.UserID) %>&type=nodoc">
                        <%= NoDocumentOnTime > 0 ? NoDocumentOnTime.ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachCuocHop, "status", "late", "uid", Sessions.UserID) %>&type=nodoc">
                        <%= NoDocumentLate > 0 ? NoDocumentLate.ToString() : "-" %>
                    </a>
                </td>
                <td class="text-in-term">
                    <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachCuocHop, "status", "ontime", "uid", Sessions.UserID) %>&type=havedoc">
                        <%= DocumentOnTime > 0 ? DocumentOnTime.ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachCuocHop, "status", "late", "uid", Sessions.UserID) %>&type=havedoc">
                        <%= DocumentLate > 0 ? DocumentLate.ToString() : "-" %>
                    </a>
                </td>
            </tr>
        </tbody>
    </table>
    <fieldset>
        <legend>TỔNG HỢP TÌNH HÌNH THEO DÕI VIỆC THỰC HIỆN Ý KIẾN CHỈ ĐẠO
        </legend>
    </fieldset>
    <asp:Repeater ID="RequestDetail" runat="server">
        <ItemTemplate>
            <table class="table table-centered table-bordered table-striped table-hover table-data" id="mainContentHolder_grdStatistic">
                <tbody>
                    <tr>
                        <th colspan="2" style="background-color: #B22222 !important; color: #fff !important;">Chưa thực hiện</th>
                        <th colspan="2" style="background-color: #5BC0DE !important; color: #fff !important;">Đang thực hiện</th>
                        <th colspan="3" style="background-color: #5CB85C !important; color: #fff !important;">Đã hoàn thành</th>
                    </tr>
                    <tr>
                        <th scope="col">Trong hạn</th>
                        <th scope="col">Quá hạn</th>
                        <th scope="col">Trong hạn</th>
                        <th scope="col">Quá hạn</th>
                        <th scope="col">Chờ xác nhận</th>
                        <th scope="col">Đúng hạn</th>
                        <th scope="col">Quá hạn</th>
                    </tr>
                    <tr>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachYKCD, "status", TrangThai.ChuaThucHienTrongHan) %>">
                                <%# Eval("NotPerformInTerm").ToInteger() > 0 ? Eval("NotPerformInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachYKCD, "status", TrangThai.ChuaThucHienQuaHan) %>">
                                <%# Eval("NotPerformOutTerm").ToInteger() > 0 ? Eval("NotPerformOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachYKCD, "status", TrangThai.DangThucHienTrongHan) %>">
                                <%# Eval("PerformingInTerm").ToInteger() > 0 ? Eval("PerformingInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachYKCD, "status", TrangThai.DangThucHienQuaHan) %>">
                                <%# Eval("PerformingOutTerm").ToInteger() > 0 ? Eval("PerformingOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachYKCD, "status", TrangThai.ChoXacNhan) %>">
                                <%# Eval("WaitToConfirm").ToInteger() > 0 ? Eval("WaitToConfirm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-in-term">
                            <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachYKCD, "status", TrangThai.DaThucHienDungHan) %>">
                                <%# Eval("DoneInTerm").ToInteger() > 0 ? Eval("DoneInTerm").ToString() : "-" %>
                            </a>
                        </td>
                        <td class="text-out-term">
                            <a href="<%= Redirector.GetLink(ViewNames.Province.DanhSachYKCD, "status", TrangThai.DaThucHienTreHan) %>">
                                <%# Eval("DoneOutTerm").ToInteger() > 0 ? Eval("DoneOutTerm").ToString() : "-" %>
                            </a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
