<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SubAgency.Master" AutoEventWireup="true" CodeBehind="ThongKe_TinhHinhGiaoViec.aspx.cs" Inherits="YKCD.SubAgency.Application.SubAgency.ThongKe_TinhHinhGiaoViec" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>THỐNG KÊ TÌNH HÌNH THỰC HIỆN Ý KIẾN CHỈ ĐẠO CỦA UBND TỈNH</legend>
    </fieldset>
    <asp:Panel ID="PanelHeadingControl" runat="server" CssClass="row search-panel">
        <div class="col-md-12">
            <div class="form-inline text-center">                
                <div class="form-group">
                    <label>Trạng thái: </label>
                    <asp:ListBox ID="TrangThai" runat="server" CssClass="form-control input-sm select2" SelectionMode="Multiple" Width="200px">
                        <Items>
                            <asp:ListItem Text="Chưa thực hiện" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Đang thực hiện" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Chờ xác nhận" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Đã hoàn thành" Value="2"></asp:ListItem>
                        </Items>
                    </asp:ListBox>
                </div>
                <div class="form-group">
                    <label>Từ ngày: </label>
                    <asp:TextBox ID="TuNgay" CssClass="form-control input-sm datepicker" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>đến ngày: </label>
                    <asp:TextBox ID="DenNgay" CssClass="form-control input-sm datepicker" runat="server"></asp:TextBox>
                </div>
                <div class="btn-group">
                    <asp:LinkButton ID="btnThongKe" CssClass="btn btn-primary btn-sm" runat="server" Text="Thống kê" OnClick="btnThongKe_Click"><span class="glyphicon glyphicon-dashboard" aria-hidden="true"></span> Thống kê</asp:LinkButton>
                    <button type="button" class="btn btn-primary btn-sm dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <span class="caret"></span>
                        <span class="sr-only">Toggle Dropdown</span>
                    </button>
                    <ul class="dropdown-menu">
                        <li>
                            <asp:LinkButton ID="btnPrint" runat="server" Text="Thống kê" OnClick="btnPrint_Click"><span class="glyphicon glyphicon-print" aria-hidden="true"></span> In báo cáo</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="btnDownLoad" runat="server" Text="Thống kê" OnClick="btnDownLoad_Click"><span class="glyphicon glyphicon-download" aria-hidden="true"></span> Tải báo cáo</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:ListView ID="lvData" runat="server" ItemPlaceholderID="itemPlaceHolder"
        OnPagePropertiesChanging="lvData_OnPagePropertiesChanging">
        <LayoutTemplate>
            <table class="table table-bordered table-striped table-hover">
                <thead>
                    <tr>
                        <th class="stt-column">STT</th>
                        <th>Văn bản</th>
                        <th>Nội dung ý kiến chỉ đạo</th>
                        <th>Đơn vị thực hiện</th>
                        <th style="width: 30%">Nội dung báo cáo</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="text-center"><%# Container.DataItemIndex + 1%>
                </td>
                <td class="nowrap text-center">
                    <a href="<%# Redirector.GetLink(ViewNames.SubAgency.ThongTinVanBan, "id", Eval("DocumentID")) %>">
                        <strong><%# Eval("Document.DocumentCode") %></strong>
                    </a>
                    <br />
                    <%# Eval("Document.ReleaseDate").ToDateString() %>
                </td>
                <td>
                    <a href="<%# Redirector.GetLink(ViewNames.SubAgency.ThongTinYKCD, "id", Eval("RequestID")) %>">
                        <%# Eval("RequestContent").ToString().Replace(Environment.NewLine, "<br />") %>
                    </a>
                    <br />
                    Thời hạn yêu cầu: <strong><%# Eval("RequiredDate").ToDateString() %></strong>
                </td>
                <td class="nowrap" style="vertical-align: top !important">
                    <%# (Parameters.DepartmentID > 0 || Parameters.YkcdUbndTinh) ? Eval("RequestStatusString") : Eval("PerformStatusString") %>
                </td>
                <td>
                    <%# DisplayReports(Eval("Reports")) %>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="alert alert-info" role="alert"><strong>Thông báo!</strong> Không có dữ liệu </div>
        </EmptyDataTemplate>
    </asp:ListView>
    <div class="text-center">
        <asp:DataPager ID="Pager" PagedControlID="lvData" runat="server" OnInit="Pager_Init"></asp:DataPager>
    </div>
</asp:Content>
