<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThongTinVanBanControl.ascx.cs" Inherits="YKCD.SubAgency.Application.Controls.ThongTinVanBanControl" %>
<fieldset>
    <legend>THÔNG TIN VĂN BẢN CHỈ ĐẠO</legend>
</fieldset>
<asp:Repeater ID="DocumentDetail" runat="server">
    <ItemTemplate>
        <table class="table table-data table-info">
            <tbody>
            <tr>
                <td width="10%" class="nowrap">
                    <strong>Số hiệu văn bản:</strong>
                </td>
                <td width="50%"><%#Eval("DocumentCode") %>
                </td>
                <td width="15%" class="nowrap">
                    <strong>Ngày ban hành:</strong>
                </td>
                <td width="25%">
                    <%#Eval("ReleaseDate").ToDateString() %>
                </td>
            </tr>
            <tr>
                <td class="nowrap">
                    <strong>Trích yếu:</strong>
                </td>
                <td colspan="3"><%#Eval("MainContent").ToString().Replace(Environment.NewLine, "<br />") %>
                </td>
            </tr>
            <tr>
                <td class="nowrap">
                    <strong>Người ký:</strong>
                </td>
                <td><%# Eval("SignerID").ToInteger() > 0 ?  Eval("Signer.FullName").ToString() : Eval("SignerName").ToString() %>
                </td>
                <td class="nowrap">
                    <strong>File đính kèm:</strong>
                </td>
                <td>
                    <%# Eval("LinkFiles") %>
                </td>
            </tr>
            <tr style="<%# Eval("CoQuyenCapNhat").ToBoolean() ? "": "display: none" %>">
                <td colspan="2"></td>
                <td colspan="2" class="text-right">
                    <a href="<%# Redirector.GetLink(ViewNames.SubAgency.YKCD_QuanLy, "pid", Eval("DocumentID")) %>" class="btn btn-primary btn-sm">
                        <span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>
                        Nhập ý kiến chỉ đạo
                    </a>
                    <a href="<%# Redirector.GetLink(ViewNames.SubAgency.VanBan_QuanLy, "id", Eval("DocumentID")) %>" class="btn btn-success btn-sm">
                        <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>
                        Cập nhật
                    </a>
                </td>
            </tr>
            </tbody>
        </table>
    </ItemTemplate>
</asp:Repeater>
