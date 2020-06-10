<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Public.Master" AutoEventWireup="true" CodeBehind="TimKiem.aspx.cs" Inherits="YKCD.Province.Application.Public.TimKiem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>TRA CỨU, TÌM KIẾM </legend>
    </fieldset>
    <table class="table">
        <tr>
            <td class="nowrap"><strong>Số hiệu văn bản</strong></td>
            <td width="30%">
                <asp:TextBox ID="SoHieuVanBan" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </td>
            <td class="nowrap text-right"><strong>Trích yếu</strong></td>
            <td width="30%">
                <asp:TextBox ID="TrichYeu" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="nowrap"><strong>Người ký</strong></td>
            <td>
                <asp:ListBox ID="NguoiKy" runat="server" CssClass="select2 form-control input-sm" SelectionMode="Multiple"></asp:ListBox>
            </td>
            <td class="nowrap text-right"><strong>Người theo dõi</strong></td>
            <td>
                <asp:ListBox ID="ChuyenVienTheoDoi" runat="server" CssClass="select2 form-control input-sm" SelectionMode="Single"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td class="nowrap"><strong>Nội dung chỉ đạo</strong></td>
            <td>
                <asp:TextBox ID="NoiDungChiDao" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </td>
            <td class="nowrap text-right"><strong>Đơn vị thực hiện</strong></td>
            <td>
                <asp:ListBox ID="DonViThucHien" runat="server" CssClass="select2 form-control input-sm" SelectionMode="Multiple"></asp:ListBox>
            </td>
        </tr>
        <tr>
           <td colspan="4" class="text-center">
                <asp:Button ID="btnSearch" CssClass="btn btn-primary" OnClick="Search_OnClick" runat="server" Text="Tìm kiếm" />
            </td>
        </tr>
    </table>
    <asp:ListView ID="lvData" runat="server" ItemPlaceholderID="itemPlaceHolder"
        OnPagePropertiesChanging="lvData_OnPagePropertiesChanging">
        <LayoutTemplate>
            <fieldset>
                <legend>KẾT QUẢ TÌM KIẾM</legend>
            </fieldset>
            <table class="table table-bordered table-striped table-hover">
                <thead>
                    <tr>
                        <th scope="col" class="text-center">STT</th>
                        <th>Văn bản</th>
                        <th>Nội dung chỉ đạo</th>
                        <th>Tình trạng</th>
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
                    <a href="<%# Redirector.GetLink(ViewNames.Public.ThongTinVanBan, "id", Eval("DocumentID")) %>">
                        <strong><%# Eval("Document.DocumentCode") %></strong>
                    </a>
                    <br />
                    <%# Eval("Document.ReleaseDate").ToDateString() %>
                </td>
                <td>
                    <a href="<%# Redirector.GetLink(ViewNames.Public.ThongTinYKCD, "id", Eval("RequestID")) %>">
                        <%# Eval("RequestContent").ToString().Replace(Environment.NewLine, "<br />") %>
                    </a>
                    <br />
                    Thời hạn yêu cầu: <strong><%# Eval("RequiredDate").ToDateString() %></strong>
                </td>
                <td class="nowrap" style="vertical-align: top !important">
                    <%# Parameters.AgencyID > 0 ? Eval("RequestStatusString") : Eval("PerformStatusString") %>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <div class="text-center">
        <asp:DataPager ID="Pager" PagedControlID="lvData" runat="server" OnInit="Pager_Init"></asp:DataPager>
    </div>
</asp:Content>
