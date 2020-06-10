<%@ Page Language="C#" MasterPageFile="~/MasterPages/Province.Master" AutoEventWireup="true" CodeBehind="VanBan_QuanLy.aspx.cs" Inherits="YKCD.Province.Application.Province.VanBan_QuanLy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>
            <asp:Label ID="lblHeaderCaption" runat="server"></asp:Label></legend>
    </fieldset>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Số hiệu văn bản <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="SoHieuVanBan" runat="server" CssClass="form-control input-sm required"></asp:TextBox>
            </div>
            <label class="col-sm-3 control-label">
                Ngày ban hành <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="NgayBanHanh" runat="server" CssClass="form-control input-sm datepicker required"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Người ký <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:ListBox ID="NguoiKy" runat="server" CssClass="select2 form-control input-sm required" Width="100%"></asp:ListBox>
            </div>
            <label class="col-sm-3 control-label">
                Người soạn thảo <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:ListBox ID="NguoiSoanThao" runat="server" CssClass="select2 form-control input-sm required" Width="100%"></asp:ListBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Trích yếu <span class="error">(*)</span>
            </label>
            <div class="col-sm-9">
                <asp:TextBox ID="TrichYeu" runat="server" CssClass="form-control input-sm required" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Loại văn bản <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:ListBox ID="LoaiVanBan" runat="server" CssClass="select2 form-control input-sm required" Width="100%"></asp:ListBox>
            </div>
            <label class="col-sm-3 control-label">
                File đính kèm <span class="error">(*)</span>
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
            <div class="col-sm-offset-6 col-sm-6">
                <asp:LinkButton ID="btnSave" runat="server" Text="<span class='glyphicon glyphicon-floppy-disk'></span>&nbsp;Lưu"
                    CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="return validateForm();" />
            </div>
        </div>
    </div>
</asp:Content>
