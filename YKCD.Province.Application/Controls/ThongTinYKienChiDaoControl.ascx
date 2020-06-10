<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThongTinYKienChiDaoControl.ascx.cs" Inherits="YKCD.Province.Application.Controls.ThongTinYKienChiDaoControl" %>
<fieldset>
    <legend>THÔNG TIN Ý KIẾN CHỈ ĐẠO</legend>
</fieldset>
<asp:Repeater ID="RequestDetail" runat="server">
    <ItemTemplate>
        <table class="table table-data table-info">
            <tbody>
                <tr>
                    <td width="10%" class="nowrap">
                        <strong>Số hiệu văn bản:</strong>
                    </td>
                    <td width="35%">
                        <a href="<%# Redirector.GetLink(ViewNames.Province.ThongTinVanBan, "id", Eval("DocumentID")) %>">
                            <%#Eval("Document.DocumentCode") %>
                        </a>
                    </td>
                    <td width="15%" class="nowrap">
                        <strong>Ngày ban hành:</strong>
                    </td>
                    <td width="35%"><%#Eval("Document.ReleaseDate").ToDateString() %>
                    </td>
                    <td class="nowrap">
                        <strong>Người ký:</strong>
                    </td>
                    <td class="nowrap"><%#Eval("Document.Signer.FullName") %>
                    </td>
                </tr>
                <tr>
                    <td class="nowrap">
                        <strong>Trích yếu:</strong>
                    </td>
                    <td colspan="3"><%#Eval("Document.MainContent").ToString().Replace(Environment.NewLine, "<br />") %>
                    </td>
                    <td class="nowrap">
                        <strong>File đính kèm:</strong>
                    </td>
                    <td class="nowrap">
                        <%# Eval("Document.LinkFiles") %>
                    </td>
                </tr>
                <tr>
                    <td class="nowrap">
                        <strong>Nội dung chỉ đạo:</strong>
                    </td>
                    <td colspan="5"><%#Eval("RequestContent").ToString().Replace(Environment.NewLine, "<br />") %>
                    </td>
                </tr>
                <tr>
                    <td class="nowrap">
                        <strong>Thực hiện:</strong>
                    </td>
                    <td colspan="5">
                        <%# Eval("PerformStatusString") %>
                    </td>
                </tr>
                <tr>
                    <td class="nowrap">
                        <strong>Thời hạn yêu cầu:</strong>
                    </td>
                    <td><%# Eval("RequiredDate").ToDateString() %>
                    </td>
                    <td class="nowrap">
                        <strong>Thời gian nhập YKCD:</strong>
                    </td>
                    <td><%# Eval("CreatedTime").ToDateTimeString() %>
                    </td>
                    <td class="nowrap">
                        <strong>Người theo dõi:</strong>
                    </td>
                    <td class="nowrap"><%# Eval("TrackerString") %>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="text-right">
                        <a href="<%# Redirector.GetLink(ViewNames.Province.BaoCao_QuanLy, "pid", Eval("RequestID")) %>"
                            class="btn btn-primary btn-sm" style="<%# Eval("CoQuyenBaoCao").ToBoolean() ? "": "display: none" %>">
                            <span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span>
                            Báo cáo
                        </a>
                        <a href="<%# Redirector.GetLink(ViewNames.Province.YKCD_QuanLy, "id", Eval("RequestID")) %>"
                            class="btn btn-success btn-sm" style="<%# Eval("CoQuyenCapNhat").ToBoolean() ? "": "display: none" %>">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"></span>
                            Cập nhật
                        </a>
                    </td>
                </tr>
            </tbody>
        </table>
    </ItemTemplate>
</asp:Repeater>
