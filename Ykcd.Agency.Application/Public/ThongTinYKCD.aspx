<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Public.Master" AutoEventWireup="true" CodeBehind="ThongTinYKCD.aspx.cs" Inherits="YKCD.Agency.Application.Public.ThongTinYKCD" EnableViewState="false" %>

<%@ Register Src="~/Controls/ThongTinYKienChiDaoControl.ascx" TagPrefix="uc1" TagName="ThongTinYKienChiDaoControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <uc1:ThongTinYKienChiDaoControl runat="server" id="ThongTinYKienChiDaoControl" />
    <fieldset>
        <legend>DANH SÁCH BÁO CÁO TÌNH HÌNH THỰC HIỆN</legend>
    </fieldset>
    <asp:ListView ID="lvData" runat="server">
        <LayoutTemplate>
            <table class="table table-centered table-bordered table-striped table-hover table-data" id="mainContentHolder_grdReports">
                <thead>
                    <tr>
                        <th scope="col">&nbsp;</th>
                        <th scope="col">Nội dung thực hiện</th>
                        <th class="nowrap" scope="col">Ngày thực hiện</th>
                        <th class="nowrap" scope="col">Ngày báo cáo</th>
                        <th class="nowrap" scope="col">File đính kèm</th>
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
                <td><%# Container.DataItemIndex + 1%></td>
                <td class="text-left">
                    <%# Eval("ReportContent").ToString().Replace(Environment.NewLine, "<br />") %>
                </td>
                <td class="text-center">
                    <%# Eval("PerformOnDate").ToDateString() %>
                </td>
                <td class="text-center">
                    <%# Eval("CreatedTime").ToDateString() %>
                </td>
                <td>
                    <%# Eval("LinkFiles") %>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="text-center text-danger">
                Chưa có báo cáo!
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>