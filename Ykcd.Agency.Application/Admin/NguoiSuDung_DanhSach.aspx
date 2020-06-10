<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="NguoiSuDung_DanhSach.aspx.cs" Inherits="YKCD.Agency.Application.Admin.NguoiSuDung_DanhSach" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>DANH SÁCH NGƯỜI SỬ DỤNG
        </legend>
    </fieldset>
    <asp:Panel ID="PanelHeadingControl" runat="server" class="row search-panel" DefaultButton="btnSearch">
        <div class="col-md-6">
            <a class="btn btn-success btn-sm" href="<%= Redirector.GetLink(ViewNames.Admin.NguoiSuDung_QuanLy) %>">
                <span class="glyphicon glyphicon-plus-sign"></span>&nbsp;Thêm mới
            </a>
            <asp:LinkButton ID="btnDelete" CssClass="btn btn-danger btn-sm" runat="server" OnClick="btnDelete_OnClick" OnClientClick="return ShowDeleteConfirm();">
                        <span class="glyphicon glyphicon-trash"></span>&nbsp;Xóa
            </asp:LinkButton>
        </div>
        <div class="col-md-3">
            <asp:ListBox ID="DonVi" runat="server" CssClass="select2 form-control input-sm" SelectionMode="Single" AutoPostBack="True" OnSelectedIndexChanged="DonVi_OnSelectedIndexChanged"></asp:ListBox>
            
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
                        <th class="text-center" width="60px"></th>
                        <th>Họ tên</th>
                        <th width="130px">Tài khoản</th>
                        <th>Chức vụ</th>
                        <th width="50px"></th>
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
                <td class="text-center">
                    <input type="checkbox" id="chkItemId" runat="server" value='<%#Eval("UserID") %>' />
                </td>
                <td>
                   <strong><%# Eval("FullName") %></strong>
                </td>
                <td class="text-center">
                    <%# Eval("UserName") %>
                </td>
                <td class="text-center">
                    <%# Eval("Position") %>
                </td>
                <td>
                    <a class="btn btn-success btn-sm" href="<%# Redirector.GetLink(ViewNames.Admin.NguoiSuDung_QuanLy, "id", Eval("UserID")) %>">
                        <span class="glyphicon glyphicon-edit"></span>
                    </a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <div class="text-center">
        <asp:DataPager ID="Pager" PagedControlID="lvData" runat="server" OnInit="Pager_Init"></asp:DataPager>
    </div>
</asp:Content>
