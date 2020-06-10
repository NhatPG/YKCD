<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Province.Master" AutoEventWireup="true" CodeBehind="VanBan_DongBo.aspx.cs" Inherits="YKCD.Province.Application.Province.VanBan_DongBo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>DANH SÁCH VĂN BẢN ĐỒNG BỘ CÓ Ý KIẾN CHỈ ĐẠO</legend>
    </fieldset>
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
                    <a href="<%# Redirector.GetLink(ViewNames.Province.VanBan_QuanLy, ParameterNames.VbdhCode, Eval("MaVBDi")) %>"><%# Eval("TrichYeu") %></a>
                </td>
                <td class="nowrap text-center">
                    <%# UserServices.GetUser(Eval("NguoiKy").ToString()) != null ? UserServices.GetUser(Eval("NguoiKy").ToString()).FullName : Eval("NguoiKy").ToString() %>
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
