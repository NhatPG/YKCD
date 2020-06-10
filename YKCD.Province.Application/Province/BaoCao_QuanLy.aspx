<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Province.Master" AutoEventWireup="true" CodeBehind="BaoCao_QuanLy.aspx.cs" Inherits="YKCD.Province.Application.Province.BaoCao_QuanLy" %>

<%@ Register Src="~/Controls/ThongTinYKienChiDaoControl.ascx" TagPrefix="uc1" TagName="ThongTinYKienChiDaoControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <uc1:ThongTinYKienChiDaoControl runat="server" ID="ThongTinYKienChiDaoControl" />
    <fieldset>
        <legend>
            <asp:Label ID="lblHeaderCaption" runat="server"></asp:Label></legend>
    </fieldset>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Nội dung thực hiện <span class="error">(*)</span>
            </label>
            <div class="col-sm-9">
                <asp:TextBox ID="NoiDungThucHien" runat="server" CssClass="form-control input-sm required" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Trạng thái thực hiện <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:DropDownList ID="TrangThai" runat="server" CssClass="form-control select2" Width="100%">
                    <Items>
                        <asp:ListItem Text="Chưa thực hiện" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Đang thực hiện" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Đã hoàn thành" Value="2"></asp:ListItem>
                    </Items>
                </asp:DropDownList>
            </div>
            <label class="col-sm-3 control-label">
                Đơn vị thực hiện
            </label>
            <div class="col-sm-3">
                <asp:ListBox ID="DonViThucHien" runat="server" CssClass="select2 form-control input-sm" Width="100%" SelectionMode="Multiple" Rows="3"></asp:ListBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Thời gian thực hiện <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="ThoiGianThucHien" runat="server" CssClass="form-control input-sm datepicker required"></asp:TextBox>
            </div>
            <label class="col-sm-3 control-label">
                File đính kèm
            </label>
            <div class="col-sm-3">
                <div class="fileupload fileupload-new" data-provides="fileupload">
                    <span class="btn btn-success btn-file btn-sm"><span class="fileupload-new"><span class="glyphicon glyphicon-open" aria-hidden="true"></span>
                        Chọn file...</span>
                        <span class="fileupload-exists">Thay đổi</span>
                        <input id="FileDinhKem" type="file" runat="server" /></span>
                    <span class="fileupload-preview"></span>
                    <a href="#" class="close fileupload-exists" data-dismiss="fileupload" style="float: none">×</a>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <asp:LinkButton ID="btnSave" runat="server" Text="<span class='glyphicon glyphicon-floppy-disk'></span>&nbsp;Lưu"
                    CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="return CheckHaveFile();" />
            </div>
        </div>
    </div>
    <div class="modal fade" tabindex="-1" role="dialog" id="thong-bao-chua-chon-file">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo chưa có file đính kèm!</h4>
                </div>
                <div class="modal-body">
                    <p>Yêu cầu đính kèm file báo cáo !!!</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary btn-sm" data-dismiss="modal">Đồng ý</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
