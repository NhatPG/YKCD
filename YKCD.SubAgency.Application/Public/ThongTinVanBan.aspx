<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Public.Master" AutoEventWireup="true" CodeBehind="ThongTinVanBan.aspx.cs" Inherits="YKCD.SubAgency.Application.Public.ThongTinVanBan" %>

<%@ Register Src="~/Controls/ThongTinVanBanControl.ascx" TagPrefix="uc1" TagName="ThongTinVanBanControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <uc1:ThongTinVanBanControl runat="server" ID="ThongTinVanBanControl" />
    <fieldset>
        <legend>DANH SÁCH Ý KIẾN CHỈ ĐẠO</legend>
    </fieldset>
    <asp:ListView ID="lvData" runat="server" ItemPlaceholderID="itemPlaceHolder">
        <LayoutTemplate>
            <table class="table table-bordered table-striped table-hover">
                <thead>
                    <tr>
                        <th  class="stt-column">STT</th>
                        <th>Nội dung chỉ đạo</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="text-center" style="vertical-align: top !important"><%# Container.DataItemIndex + 1%>
                </td>
                <td style="vertical-align: top !important">
                    <a href="<%# Redirector.GetLink(ViewNames.Public.ThongTinYKCD, "id", Eval("RequestID")) %>">
                        <%# Eval("RequestContent").ToString().Replace(Environment.NewLine, "<br />") %></a><br />
                    Thời hạn yêu cầu: <strong><%# Eval("RequiredDate").ToDateString() %></strong>
                </td>
                <td class="nowrap" style="vertical-align: top !important">
                    <%# Eval("PerformStatusString") %>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
