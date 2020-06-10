<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SubAgency.Master" AutoEventWireup="true" CodeBehind="ThongKe_TheoChuyenVien.aspx.cs" Inherits="YKCD.SubAgency.Application.SubAgency.ThongKe_TheoChuyenVien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>THỐNG KÊ TÌNH HÌNH THỰC HIỆN Ý KIẾN CHỈ ĐẠO THEO CHUYÊN VIÊN THEO DÕI</legend>
    </fieldset>
    <asp:Panel ID="PanelHeadingControl" runat="server" CssClass="row search-panel">
        <div class="col-md-12">
            <div class="form-inline text-center">
                <div class="form-group">
                    <label>Từ ngày: </label>
                    <asp:TextBox ID="TuNgay" CssClass="form-control input-sm datepicker" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>đến ngày: </label>
                    <asp:TextBox ID="DenNgay" CssClass="form-control input-sm datepicker" runat="server"></asp:TextBox>
                </div>
                <asp:Button ID="btnThongKe" CssClass="btn btn-primary btn-sm" runat="server" Text="Thống kê" OnClick="btnThongKe_OnClick" />
            </div>
        </div>
    </asp:Panel>
    <asp:ListView ID="lvData" runat="server">
        <LayoutTemplate>
            <table class="table table-bordered table-striped table-hover">
                <thead>
                    <tr>
                        <th rowspan="2" scope="col" class="text-center">STT</th>
                        <th rowspan="2" scope="col">Người theo dõi</th>
                        <th colspan="2" class="text-center">Chưa thực hiện</th>
                        <th colspan="2" class="text-center">Đang thực hiện</th>
                        <th colspan="3" class="text-center">Đã hoàn thành</th>
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
                </thead>
                <tbody>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                    <tr class="total-row" runat="server">
                        <td class="text-center" colspan="2">
                            <strong>Tổng cộng</strong>
                        </td>
                        <td class="text-in-term"><%= tongCong.NotPerformInTerm > 0 ? tongCong.NotPerformInTerm.ToString() : "-" %>
                        </td>
                        <td class="text-out-term"><%= tongCong.NotPerformOutTerm > 0 ? tongCong.NotPerformOutTerm.ToString() : "-" %>
                        </td>
                        <td class="text-in-term"><%= tongCong.PerformingInTerm > 0 ? tongCong.PerformingInTerm.ToString() : "-" %>
                        </td>
                        <td class="text-out-term"><%= tongCong.PerformingOutTerm > 0 ? tongCong.PerformingOutTerm.ToString() : "-" %>
                        </td>
                        <td class="text-out-term"><%= tongCong.WaitToConfirm > 0 ? tongCong.WaitToConfirm.ToString() : "-" %>
                        </td>
                        <td class="text-in-term"><%= tongCong.DoneInTerm > 0 ? tongCong.DoneInTerm.ToString() : "-" %>
                        </td>
                        <td class="text-out-term"><%= tongCong.DoneOutTerm > 0 ? tongCong.DoneOutTerm.ToString() : "-" %>
                        </td>
                    </tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <%# AddGroupingRowIfGroupNameHasChanged() %>
                <td><%# Container.DataItemIndex + 1%>
                </td>
                <td class="text-left">
                    <strong>
                        <a href="<%# Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID")) %>"><%# Eval("ObjectName") %></a>
                    </strong>
                </td>
                <td class="text-in-term">
                    <a href="<%# Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.ChuaThucHienTrongHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("NotPerformInTerm").ToInteger() > 0 ? Eval("NotPerformInTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.ChuaThucHienQuaHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("NotPerformOutTerm").ToInteger() > 0 ? Eval("NotPerformOutTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-in-term">
                    <a href="<%# Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DangThucHienTrongHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("PerformingInTerm").ToInteger() > 0 ? Eval("PerformingInTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DangThucHienQuaHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("PerformingOutTerm").ToInteger() > 0 ? Eval("PerformingOutTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.ChoXacNhan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("WaitToConfirm").ToInteger() > 0 ? Eval("WaitToConfirm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-in-term">
                    <a href="<%# Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DaThucHienDungHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("DoneInTerm").ToInteger() > 0 ? Eval("DoneInTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.SubAgency.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DaThucHienTreHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("DoneOutTerm").ToInteger() > 0 ? Eval("DoneOutTerm").ToString() : "-" %>
                    </a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
