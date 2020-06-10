<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="NguoiSuDung_QuanLy.aspx.cs" Inherits="YKCD.Province.Application.Admin.NguoiSuDung_QuanLy" %>

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
                Họ tên <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="HoTen" runat="server" CssClass="form-control input-sm required"></asp:TextBox>
            </div>
            <label class="col-sm-3 control-label">
                Đơn vị <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:ListBox ID="DonVi" runat="server" CssClass="select2 form-control input-sm" Width="100%"></asp:ListBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Tên tài khoản <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="TenTaiKhoan" runat="server" CssClass="form-control input-sm required"></asp:TextBox>
            </div>
            <label class="col-sm-3 control-label">
                Mật khẩu <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="MatKhau" runat="server" CssClass="form-control input-sm" TextMode="Password"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Chức vụ <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="ChucVu" runat="server" CssClass="form-control input-sm required"></asp:TextBox>
            </div>
            <label class="col-sm-3 control-label">
                Loại tài khoản <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:ListBox ID="LoaiTaiKhoan" runat="server" CssClass="select2 form-control input-sm" Width="100%">
                    <Items>
                        <asp:ListItem Text="Lãnh đạo đơn vị" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Lãnh đạo văn phòng" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Chuyên viên" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Quản trị hệ thống" Value="1"></asp:ListItem>
                    </Items>
                </asp:ListBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Thứ tự hiển thị <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="ThuTuHienThi" runat="server" CssClass="form-control input-sm required"></asp:TextBox>
            </div>
            <label class="col-sm-3 control-label">
                
            </label>
            <div class="col-sm-3">
                <div class="checkbox checkbox-primary">
                    <asp:CheckBox ID="ConSuDung" runat="server" />
                    <label for="<%= ConSuDung.ClientID %>">
                        Còn sử dụng
                    </label>
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
