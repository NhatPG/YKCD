<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Province.Master" AutoEventWireup="true" CodeBehind="DanhSachCuocHop.aspx.cs" Inherits="YKCD.Province.Application.Province.DanhSachCuocHop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>DANH SÁCH CUỘC HỌP CÓ THÔNG BÁO KẾT LUẬN<br />
            <small>
                <asp:Literal ID="lbSmallHeader" runat="server"></asp:Literal>
            </small>
        </legend>
    </fieldset>
    <asp:Panel ID="Panel1" runat="server" CssClass="row search-panel">
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
    <asp:Panel ID="PanelHeadingControl" runat="server" CssClass="row search-panel" DefaultButton="btnSearch">
        <div class="col-md-9">
        </div>
        <div class="col-md-3">
            <div class="input-group input-group-sm">
                <asp:TextBox ID="txtSearchText" ClientIDMode="Static" runat="server" class="form-control" placeholder="Chuỗi tìm kiếm"></asp:TextBox>
                <span class="input-group-btn">
                    <asp:LinkButton ID="btnReload" ClientIDMode="Static" CssClass="btn btn-default display-none" runat="server" OnClick="btnReload_OnClick">
                        <span class="glyphicon glyphicon-remove"></span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnSearch" class="btn btn-default" runat="server" OnClick="btnSearch_Click">
                        <span class="glyphicon glyphicon-search"></span>
                    </asp:LinkButton>
                </span>
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
                        <th>Nội dung cuộc họp</th>
                        <th class="nowrap">Thời gian họp</th>
                        <th class="nowrap">Lãnh đạo chủ trì</th>
                        <th class="nowrap">Chuyên viên theo dõi</th>
                        <th class="nowrap">Thông báo kết luận</th>
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
                <td>
                    <a href="<%# Redirector.GetLink(ViewNames.Province.VanBan_ThongBaoKetLuan, "id", Eval("MettingID")) %>">
                        <%# Eval("MettingContent") %>
                    </a>
                </td>
                <td class="text-center">
                    <%# Eval("Time").ToDateString() %>
                </td>
                <td class="text-center nowrap">
                    <%# Eval("TenNguoiChuTri") %>
                </td>
                <td class="text-center">
                    <%# Eval("TenNguoiDangKy") %>
                </td>
                <td class="text-center <%# GetColor(Eval("Time").ToDateTime(), Eval("DocumentID").ToInteger()) %>">
                    <a href="<%# Redirector.GetLink(ViewNames.Province.ThongTinVanBan, "id", Eval("DocumentID")) %>">
                        <strong><%# Eval("DocumentCode") %></strong>
                    </a>
                    <br/>
                    <%# Eval("ReleasedDate") %>
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
