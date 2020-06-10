<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Province.Master" AutoEventWireup="true" CodeBehind="ThongKe_TheoChuyenVien.aspx.cs" Inherits="YKCD.Province.Application.Province.ThongKe_TheoChuyenVien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                <div class="btn-group">
                    <asp:LinkButton ID="btnThongKe" CssClass="btn btn-primary btn-sm" runat="server" Text="Thống kê" OnClick="btnThongKe_OnClick"><span class="glyphicon glyphicon-dashboard" aria-hidden="true"></span> Thống kê</asp:LinkButton>
                    <button type="button" class="btn btn-primary btn-sm dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <span class="caret"></span>
                        <span class="sr-only">Toggle Dropdown</span>
                    </button>
                    <ul class="dropdown-menu">
                        <li>
                            <asp:LinkButton ID="btnPrint" runat="server" Text="Thống kê" OnClick="btnPrint_OnClick"><span class="glyphicon glyphicon-print" aria-hidden="true"></span> In báo cáo</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="btnDownLoad" runat="server" Text="Thống kê" OnClick="btnDownLoad_OnClick"><span class="glyphicon glyphicon-download" aria-hidden="true"></span> Tải báo cáo</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:ListView ID="lvData" runat="server">
        <LayoutTemplate>
            <table class="table table-bordered table-striped table-hover">
                <thead>
                    <tr>
                        <th rowspan="2" scope="col" class="text-center" style="vertical-align: middle;">STT</th>
                        <th rowspan="2" scope="col" style="vertical-align: middle;">Chuyên viên theo dõi</th>
                        <th colspan="3" class="text-center" style="background-color: #F09F1A !important; color: #fff !important;">Thông báo kết luận</th>
                        <th colspan="2" class="text-center" style="background-color: #B22222 !important; color: #fff !important;">Chưa thực hiện</th>
                        <th colspan="2" class="text-center" style="background-color: #5BC0DE !important; color: #fff !important;">Đang thực hiện</th>
                        <th colspan="3" class="text-center" style="background-color: #5CB85C !important; color: #fff !important;">Đã hoàn thành</th>
                    </tr>
                    <tr>
                        <th scope="col" style="vertical-align: middle;">Chưa ban hành quá hạn</th>
                        <th scope="col" style="vertical-align: middle;">Đã ban hành đúng hạn</th>
                        <th scope="col" style="vertical-align: middle;">Đã ban hành quá hạn</th>
                        <th scope="col" style="vertical-align: middle;">Trong hạn</th>
                        <th scope="col" style="vertical-align: middle;">Quá hạn</th>
                        <th scope="col" style="vertical-align: middle;">Trong hạn</th>
                        <th scope="col" style="vertical-align: middle;">Quá hạn</th>
                        <th scope="col" style="vertical-align: middle;">Chờ xác nhận</th>
                        <th scope="col" style="vertical-align: middle;">Đúng hạn</th>
                        <th scope="col" style="vertical-align: middle;">Quá hạn</th>
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
                        <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID")) %>"><%# Eval("ObjectName") %></a>
                    </strong>
                </td>
                <td class="text-out-term" width="8%">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachCuocHop, "uid", Eval("ObjectID"), "status", "late", "type", "nodoc") %>">
                        <%# Eval("NoDocumentLate").ToInteger() > 0 ? Eval("NoDocumentLate").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-in-term" width="8%">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachCuocHop, "uid", Eval("ObjectID"), "status", "ontime", "type", "havedoc") %>">
                        <%# Eval("DocumentOnTime").ToInteger() > 0 ? Eval("DocumentOnTime").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term" width="8%">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachCuocHop, "uid", Eval("ObjectID"), "status", "late", "type", "havedoc") %>">
                        <%# Eval("DocumentLate").ToInteger() > 0 ? Eval("DocumentLate").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-in-term" width="8%">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.ChuaThucHienTrongHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("NotPerformInTerm").ToInteger() > 0 ? Eval("NotPerformInTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term" width="9%">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.ChuaThucHienQuaHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("NotPerformOutTerm").ToInteger() > 0 ? Eval("NotPerformOutTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-in-term" width="8%">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DangThucHienTrongHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("PerformingInTerm").ToInteger() > 0 ? Eval("PerformingInTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term" width="8%">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DangThucHienQuaHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("PerformingOutTerm").ToInteger() > 0 ? Eval("PerformingOutTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term" width="8%">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.ChoXacNhan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("WaitToConfirm").ToInteger() > 0 ? Eval("WaitToConfirm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-in-term" width="8%">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DaThucHienDungHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("DoneInTerm").ToInteger() > 0 ? Eval("DoneInTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term" width="8%">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DaThucHienTreHan, "fromDate", TuNgay.Text.Replace("/", "-")) %>">
                        <%# Eval("DoneOutTerm").ToInteger() > 0 ? Eval("DoneOutTerm").ToString() : "-" %>
                    </a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
