<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="DonVi_QuanLy.aspx.cs" Inherits="YKCD.Province.Application.Admin.DonVi_QuanLy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>
            <asp:Label ID="lblHeaderCaption" runat="server"></asp:Label>
        </legend>
    </fieldset>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Tên đơn vị <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="TenDonVi" runat="server" CssClass="form-control input-sm required"></asp:TextBox>
            </div>
            <label class="col-sm-3 control-label">
                Nhóm đơn vị <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:ListBox ID="NhomDonVi" runat="server" CssClass="select2 form-control input-sm" Width="100%"></asp:ListBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Tên đăng nhập
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="TenDangNhap" runat="server" CssClass="form-control input-sm"></asp:TextBox>
            </div>
            <label class="col-sm-3 control-label">
                Mật khẩu
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="MatKhau" runat="server" TextMode="Password" CssClass="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Địa chỉ dịch vụ
            </label>
            <div class="col-sm-9">
                <asp:TextBox ID="ServiceUrl" runat="server" CssClass="form-control input-sm"></asp:TextBox>
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
                    <asp:CheckBox ID="HienThiThongKe" runat="server" />
                    <label for="<%= HienThiThongKe.ClientID %>">
                        Hiển thị thống kê
                    </label>
                </div>
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
