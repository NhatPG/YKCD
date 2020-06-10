<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Province.Master" AutoEventWireup="true" CodeBehind="YKCD_QuanLy.aspx.cs" Inherits="YKCD.Province.Application.Province.YKCD_QuanLy" %>

<%@ Register Src="~/Controls/ThongTinVanBanControl.ascx" TagPrefix="uc1" TagName="ThongTinVanBanControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <uc1:ThongTinVanBanControl runat="server" ID="ThongTinVanBanControl" />
    <fieldset>
        <legend>
            <asp:Label ID="lblHeaderCaption" runat="server"></asp:Label></legend>
    </fieldset>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Nội dung chỉ đạo <span class="error">(*)</span>
            </label>
            <div class="col-sm-9">
                <asp:TextBox ID="NoiDungChiDao" runat="server" CssClass="form-control input-sm required" TextMode="MultiLine" Rows="6"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Thời hạn <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="ThoiHan" runat="server" CssClass="form-control input-sm datepicker required"></asp:TextBox>
            </div>
             <label class="col-sm-3 control-label">
                Chuyên viên theo dõi <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:ListBox ID="ChuyenVienTheoDoi" runat="server" CssClass="select2 form-control input-sm required" Width="100%" SelectionMode="Multiple"></asp:ListBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Đơn vị thực hiện <span class="error">(*)</span>
            </label>
            <div class="col-sm-9">
                <asp:ListBox ID="DonViThucHien" runat="server" CssClass="select2 form-control input-sm required" Width="100%" SelectionMode="Multiple" Rows="3"></asp:ListBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <asp:LinkButton ID="btnSave" runat="server" Text="<span class='glyphicon glyphicon-floppy-disk'></span>&nbsp;Lưu"
                    CssClass="btn btn-success btn-sm" OnClick="btnSave_Click" OnClientClick="return validateForm();" />
                <asp:LinkButton ID="btnSaveAndCreate" runat="server" Text="<span class='glyphicon glyphicon-floppy-disk'></span>&nbsp;Lưu và Thêm mới"
                    CssClass="btn btn-info btn-sm" OnClick="btnSaveAndCreate_Click" OnClientClick="return validateForm();" />
            </div>
        </div>
    </div>
</asp:Content>
