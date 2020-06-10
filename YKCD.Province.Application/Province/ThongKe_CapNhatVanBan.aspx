<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Province.Master" AutoEventWireup="true" CodeBehind="ThongKe_CapNhatVanBan.aspx.cs" Inherits="YKCD.Province.Application.Province.ThongKe_CapNhatVanBan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>THỐNG KÊ TÌNH HÌNH CẬP NHẬT VĂN BẢN CHỈ ĐẠO</legend>
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
    <asp:ListView ID="lvData" runat="server" ItemPlaceholderID="itemPlaceHolder">
        <LayoutTemplate>
            <table class="table table-bordered table-striped table-hover">
                <thead>
                    <tr>
                        <th scope="col" class="text-center">STT</th>
                        <th>Số hiệu</th>
                        <th>Trích yếu</th>
                        <th>Người ký</th>
                        <th class="nowrap">Ngày cập nhật</th>
                        <th>Ghi chú</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <%# AddGroupingRowIfGroupNameHasChanged() %>
                <td class="text-center"><%# Container.DataItemIndex + 1%>
                </td>
                <td class="nowrap text-center">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.ThongTinVanBan, "id", Eval("DocumentID")) %>">
                        <strong><%# Eval("DocumentCode") %></strong>
                    </a>
                    <br />
                    <%# Eval("ReleaseDate").ToDateString() %>
                </td>
                <td>
                    <a href="<%# Redirector.GetLink(ViewNames.Province.ThongTinVanBan, "id", Eval("DocumentID")) %>"><%# Eval("MainContent") %></a>
                </td>
                <td class="nowrap">
                    <%# Eval("Signer.FullName") %>
                </td>
                <td class="text-center">
                    <%# Eval("CreatedTime").ToDateString() %>
                </td>
                <td class="text-center nowrap">
                    <%# ShowDocumentUpdateStatus(Eval("ReleaseDate").ToDateTime().Date, Eval("CreatedTime").ToDateTime().Date) %>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
