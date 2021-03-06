﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Public.Master" AutoEventWireup="true" CodeBehind="VanBan_DanhSach.aspx.cs" Inherits="YKCD.SubAgency.Application.Public.VanBan_DanhSach" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>DANH SÁCH VĂN BẢN CHỈ ĐẠO<br />
            <small>
                <asp:Literal ID="SmallHeader" runat="server"></asp:Literal>
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
                        <th scope="col" class="text-center stt-column">STT</th>
                        <th>Số hiệu</th>
                        <th>Trích yếu</th>
                        <th>Người ký</th>
                        <th class="nowrap">Số YKCD</th>
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
                        <strong><%# Eval("DocumentCode") %></strong>
                    </a>
                    <br />
                    <%# Eval("ReleaseDate").ToDateString() %>
                </td>
                <td>
                    <a href="<%# Redirector.GetLink(ViewNames.Public.ThongTinVanBan, "id", Eval("DocumentID")) %>">
                        <%# Eval("MainContent").ToString().Replace(Environment.NewLine, "<br />") %></a>
                </td>
                <td class="nowrap text-center">
                    <%# Eval("SignerID").ToInteger() > 0 ?  Eval("Signer.FullName") : Eval("SignerName") %>
                </td>
                <td class="text-center">
                    <%# Eval("Requests.Count") %>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <div class="text-center">
        <asp:DataPager ID="Pager" PagedControlID="lvData" runat="server" OnInit="Pager_Init"></asp:DataPager>
    </div>
</asp:Content>
