<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XacNhanHoanThanh.aspx.cs" Inherits="YKCD.SubAgency.Application.SubAgency.XacNhanHoanThanh" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title" id="myModalLabel">XÁC NHẬN TÌNH TRẠNG THỰC HIỆN Ý KIẾN CHỈ ĐẠO
            </h4>
        </div>
        <div class="modal-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        Trạng thái thực hiện <span class="error">(*)</span>
                    </label>
                    <div class="col-sm-8">
                        <asp:DropDownList ID="TrangThai" runat="server" CssClass="form-control select2" Width="100%">
                            <Items>
                                <asp:ListItem Text="Chưa thực hiện" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Đang thực hiện" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Đã hoàn thành" Value="2"></asp:ListItem>
                            </Items>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        Thời gian thực hiện
                    </label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="NgayHoanThanh" runat="server" CssClass="form-control input-sm datepicker"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">
                        Thông tin thực hiện
                    </label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="BaoCaoThucHien" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-4 col-sm-8">
                        <asp:LinkButton ID="btnSave" runat="server" Text="<span class='glyphicon glyphicon-floppy-disk'></span>&nbsp;Lưu"
                    CssClass="btn btn-success" OnClick="btnSave_OnClick" OnClientClick="return validatePopupForm();" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

