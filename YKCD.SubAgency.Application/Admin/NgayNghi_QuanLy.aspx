<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="NgayNghi_QuanLy.aspx.cs" Inherits="YKCD.SubAgency.Application.Admin.NgayNghi_QuanLy" %>
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
                Tên ngày nghỉ, lễ <span class="error">(*)</span>
            </label>
            <div class="col-sm-9">
                <asp:TextBox ID="TenNgayNghi" runat="server" CssClass="form-control input-sm required"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Từ ngày <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="TuNgay" runat="server" CssClass="form-control input-sm datepicker required"></asp:TextBox>
            </div>
            <label class="col-sm-3 control-label">
                Đến ngày <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="DenNgay" runat="server" CssClass="form-control input-sm datepicker required"></asp:TextBox>
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
