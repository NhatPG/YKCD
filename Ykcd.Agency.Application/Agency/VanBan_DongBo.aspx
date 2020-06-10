<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Agency.Master" AutoEventWireup="true" CodeBehind="VanBan_DongBo.aspx.cs" Inherits="YKCD.Agency.Application.Agency.VanBan_DongBo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>DANH SÁCH VĂN BẢN ĐỒNG BỘ CÓ Ý KIẾN CHỈ ĐẠO</legend>
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
                <div class="form-group">
                    <div class="checkbox checkbox-primary">
                    <asp:CheckBox ID="ChiLayVbCaNhan" runat="server" />
                    <label for="<%= ChiLayVbCaNhan.ClientID %>">
                        Chỉ lấy văn bản cá nhân?
                    </label>
                </div>
                </div>
                <div class="btn-group">
                    <asp:LinkButton ID="btnThongKe" CssClass="btn btn-primary btn-sm" runat="server" Text="Thống kê" OnClick="btnThongKe_Click"><span class="glyphicon glyphicon-dashboard" aria-hidden="true"></span> Lấy văn bản</asp:LinkButton>
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
                        <th scope="col" class="text-center">STT</th>
                        <th>Số hiệu</th>
                        <th>Trích yếu</th>
                        <th>Người ký</th>
                        <th class="nowrap">Người soạn thảo</th>
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
                    <strong><%# Eval("SoKyHieu") %></strong>
                    <br />
                    <%# Eval("NgayPhatHanh").ToDateString() %>
                </td>
                <td>
                    <a href="<%# Redirector.GetLink(ViewNames.Agency.VanBan_QuanLy, ParameterNames.VbdhCode, Eval("MaVBDi")) %>"><%# Eval("TrichYeu").ToString().Replace(Environment.NewLine, "<br />") %></a>
                </td>
                <td class="nowrap text-center">
                    <%# UserServices.GetUser(Eval("NguoiKy").ToString()).FullName%>
                </td>
                <td class="nowrap text-center">
                    <%# Eval("NguoiSoanThao")%>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="alert alert-info" role="alert">Không có văn bản nào có ý kiến chỉ đạo ở hệ thống <strong>Hồ sơ công việc</strong> ! </div>
        </EmptyDataTemplate>
    </asp:ListView>
    <div class="text-center">
        <asp:DataPager ID="Pager" PagedControlID="lvData" runat="server" OnInit="Pager_Init"></asp:DataPager>
    </div>
</asp:Content>
