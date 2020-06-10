<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Province.Master" AutoEventWireup="true" CodeBehind="ThongKe_KinhTeNganhKntc.aspx.cs" Inherits="YKCD.Province.Application.Province.ThongKe_KinhTeNganhKntc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>THỐNG KÊ TÌNH HÌNH THỰC HIỆN Ý KIẾN CHỈ ĐẠO LĨNH VỰC KINH TẾ NGÀNH - KNTC<br />
            <small>Người theo dõi: Đ/c Đặng Ngọc Trân - Phó Chánh Văn phòng UBND tỉnh</small>
        </legend>
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
    <p>
        <h4>
            Trong tuần  lĩnh vực kinh tế ngành và khiếu nại tố cáo đã ban hành <asp:Literal ID="lbYkcdBanHanhTrongTuan" runat="server"></asp:Literal> ý kiến chỉ đạo.
        </h4>
    </p>
    <p>
        <h4>Tính đến nay có <asp:Literal ID="lbChuaThucHien" runat="server"></asp:Literal> ý kiến chỉ đạo chưa thực hiện (trong đó, chưa đến hạn <asp:Literal ID="lbChuaDenHan" runat="server"></asp:Literal> ý kiến, trễ hạn <asp:Literal ID="lbTreHan" runat="server"></asp:Literal> ý kiến); và <asp:Literal ID="lbDangThucHien" runat="server"></asp:Literal> ý kiến chỉ đạo đang thực hiện.</h4>
    </p>
    <asp:ListView ID="lvData" runat="server">
        <LayoutTemplate>
            <table class="table table-bordered table-striped table-hover">
                <thead>
                    <tr>
                        <th rowspan="2" scope="col" class="text-center">STT</th>
                        <th rowspan="2" scope="col">Chuyên viên theo dõi</th>
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
                        <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID")) %>"><%# Eval("ObjectName") %></a>
                    </strong>
                </td>
                <td class="text-in-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.ChuaThucHienTrongHan) %>">
                        <%# Eval("NotPerformInTerm").ToInteger() > 0 ? Eval("NotPerformInTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.ChuaThucHienQuaHan) %>">
                        <%# Eval("NotPerformOutTerm").ToInteger() > 0 ? Eval("NotPerformOutTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-in-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DangThucHienTrongHan) %>">
                        <%# Eval("PerformingInTerm").ToInteger() > 0 ? Eval("PerformingInTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DangThucHienQuaHan) %>">
                        <%# Eval("PerformingOutTerm").ToInteger() > 0 ? Eval("PerformingOutTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.ChoXacNhan) %>">
                        <%# Eval("WaitToConfirm").ToInteger() > 0 ? Eval("WaitToConfirm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-in-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DaThucHienDungHan) %>">
                        <%# Eval("DoneInTerm").ToInteger() > 0 ? Eval("DoneInTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.DanhSachYKCD, ParameterNames.StaffID, Eval("ObjectID"), "status", TrangThai.DaThucHienTreHan) %>">
                        <%# Eval("DoneOutTerm").ToInteger() > 0 ? Eval("DoneOutTerm").ToString() : "-" %>
                    </a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>

</asp:Content>
