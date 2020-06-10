<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="LoaiVanBan_QuanLy.aspx.cs" Inherits="YKCD.Province.Application.Admin.LoaiVanBan_QuanLy" %>

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
                Tên loại văn bản <span class="error">(*)</span>
            </label>
            <div class="col-sm-7">
                <asp:TextBox ID="TenLoaiVanBan" runat="server" CssClass="form-control input-sm required"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Thứ tự hiển thị <span class="error">(*)</span>
            </label>
            <div class="col-sm-7">
                <asp:TextBox ID="ThuTuHienThi" runat="server" CssClass="form-control input-sm required"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-7">
                <asp:LinkButton ID="btnSave" runat="server" Text="<span class='glyphicon glyphicon-floppy-disk'></span>&nbsp;Lưu"
                    CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="return validateForm();" />
            </div>
        </div>
    </div>
</asp:Content>
