<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Province.Master" AutoEventWireup="true" CodeBehind="VanBan_ThongBaoKetLuan.aspx.cs" Inherits="YKCD.Province.Application.Province.VanBan_ThongBaoKetLuan" %>
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
                Chọn văn bản <span class="error">(*)</span>
            </label>
            <div class="col-sm-9">
                <asp:ListBox ID="VanBan" runat="server" CssClass="select2 form-control input-sm required" Width="100%"></asp:ListBox>
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
