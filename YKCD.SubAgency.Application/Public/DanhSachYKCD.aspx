<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Public.Master" AutoEventWireup="true" CodeBehind="DanhSachYKCD.aspx.cs" Inherits="YKCD.SubAgency.Application.Public.DanhSachYKCD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend><asp:Literal ID="lbMainHeader" runat="server" Text="DANH SÁCH Ý KIẾN CHỈ ĐẠO"></asp:Literal><br />
            <small>
                <asp:Literal ID="lbSmallHeader" runat="server"></asp:Literal>
            </small>
        </legend>
    </fieldset>
    <asp:ListView ID="lvData" runat="server" ItemPlaceholderID="itemPlaceHolder"
                  OnPagePropertiesChanging="lvData_OnPagePropertiesChanging">
        <LayoutTemplate>
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
                    </a><br />
                    Thời hạn yêu cầu: <strong><%# Eval("RequiredDate").ToDateString() %></strong>
                </td>
                <td class="nowrap" style="vertical-align: top !important">
                    <%# (Parameters.DepartmentID > 0 || Parameters.YkcdUbndTinh) ? Eval("RequestStatusString") : Eval("PerformStatusString") %>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <div class="text-center">
        <asp:DataPager ID="Pager" PagedControlID="lvData" runat="server" OnInit="Pager_Init"></asp:DataPager>
    </div>
</asp:Content>
