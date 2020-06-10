<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Agency.Master" AutoEventWireup="true" CodeBehind="DanhSachYKCD.aspx.cs" Inherits="YKCD.Agency.Application.Agency.DanhSachYKCD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>DANH SÁCH Ý KIẾN CHỈ ĐẠO<br />
            <small>
                <asp:Literal ID="lbSmallHeader" runat="server"></asp:Literal>
            </small>
        </legend>
    </fieldset>
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
                    <a href="<%# Redirector.GetLink(ViewNames.Agency.ThongTinVanBan, "id", Eval("DocumentID")) %>">
                        <strong><%# Eval("Document.DocumentCode") %></strong>
                    </a>
                    <br />
                    <%# Eval("Document.ReleaseDate").ToDateString() %>
                </td>
                <td>
                    <a href="<%# Redirector.GetLink(ViewNames.Agency.ThongTinYKCD, "id", Eval("RequestID")) %>">
                        <%# Eval("RequestContent").ToString().Replace(Environment.NewLine, "<br />") %>
                    </a>
                    <br />
                    Thời hạn yêu cầu: <strong><%# Eval("RequiredDate").ToDateString() %></strong></br>
                    <div class="text-danger"><%# DisplayReports(Eval("Reports")) %></div>
                     
                </td>
                <td class="nowrap" style="vertical-align: top !important">
                    <%# (Parameters.DepartmentID > 0 || Parameters.YkcdUbndTinh) ? Eval("RequestStatusString") : Eval("PerformStatusString") %>
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
