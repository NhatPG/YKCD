<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DangNhap.aspx.cs" Inherits="YKCD.SubAgency.Application.Public.DangNhap" %>
<%@ Register Src="~/MasterPages/Controls/Public_NavBarControl.ascx" TagPrefix="asp" TagName="Public_NavBarControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hệ thống theo dõi ý kiến chỉ đạo và văn bản ban hành</title>
    <link rel="shortcut icon" href="<%=Page.ResolveClientUrl("~/Images/quochuy.png")%>" />
    <link href="<%=Page.ResolveClientUrl("~/Styles/bootstrap.min.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveClientUrl("~/Styles/sb-admin-2.min.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveClientUrl("~/Styles/font-awesome.min.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveClientUrl("~/Styles/bootstrap-datepicker.min.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveClientUrl("~/Styles/panel-with-tabs.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveClientUrl("~/Styles/select2.min.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveClientUrl("~/Styles/bootstrap-file-upload.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveClientUrl("~/Styles/awesome-bootstrap-checkbox.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=Page.ResolveClientUrl("~/Styles/common.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=Page.ResolveClientUrl("~/Scripts/jquery-1.9.1.min.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveClientUrl("~/Scripts/jquery.validate.min.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveClientUrl("~/Scripts/bootstrap.min.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveClientUrl("~/Scripts/sb-admin-2.min.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveClientUrl("~/Scripts/bootstrap-datepicker.min.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveClientUrl("~/Scripts/bootstrap-file-upload.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveClientUrl("~/Scripts/metisMenu.min.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveClientUrl("~/Scripts/common.js")%>" type="text/javascript"></script>
    <script src="<%=Page.ResolveClientUrl("~/Scripts/select2.min.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .form-signin {
            max-width: 450px;
            padding: 39px 29px 29px;
            margin: 0 auto 20px;
            margin-top: 50px;
            background-color: #fff;
            border: 1px solid #e5e5e5;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px; /*border-radius: 5px;*/
            -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            box-shadow: 0 1px 2px rgba(0,0,0,.05);
        }
    </style>
</head>
<body style="padding-bottom: 40px; background-color: #f5f5f5;">
<form id="form1" runat="server" onsubmit="return validateForm()">
    <asp:BannerControl runat="server" ID="BannerControl" />
    <div id="wrapper">
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0; background-color: #293955;">
            <asp:Public_NavBarControl runat="server" ID="Public_NavBarControl" />
        </nav>
        <div class="form-signin" style="min-height: 294px; padding-top: 10px;">
            <fieldset>
                <legend>ĐĂNG NHẬP
                </legend>
            </fieldset>
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-4 control-label">Tên đăng nhập</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="TenDangNhap" runat="server" CssClass="form-control" placeholder="Tên đăng nhập"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-4 control-label">Mật khẩu</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="MatKhau" runat="server" CssClass="form-control" TextMode="Password" placeholder="Mật khẩu"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-4 col-sm-8">
                        <div class="checkbox checkbox-primary">
                            <asp:CheckBox ID="IsSSO" runat="server" />
                            <label for="<%= IsSSO.ClientID %>">Sử dụng SSO</label>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-4 col-sm-8">
                        <asp:Button ID="btnDangNhap" runat="server" CssClass="btn btn-success" OnClick="DangNhap_OnClick" Text="Đăng nhập" />
                        <br />
                        <asp:Label CssClass="text-error" ID="lblMessage" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>
</body>
</html>
