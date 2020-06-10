﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Province_NarvBarControl.ascx.cs" Inherits="YKCD.Province.Application.MasterPages.Controls.Province_NarvBarControl" %>
<ul class="nav navbar-top-links navbar-right">
    <li class="<%= Request.Path.Contains(ViewNames.Province.TimKiem) ? "active": ""%>">
        <a href="<%= Redirector.GetLink(ViewNames.Province.TimKiem) %>">
            <strong><span class="glyphicon glyphicon-search" aria-hidden="true"></span>Tìm kiếm</strong>
        </a>
    </li>
    <li class="<%= Request.Path.Contains(ViewNames.Province.TimKiem) ? "": "active"%>">
        <a href="<%= Redirector.GetLink(ViewNames.Province.BangThongKe) %>">
            <strong><span class="glyphicon glyphicon-bookmark" aria-hidden="true"></span>Ý kiến chỉ đạo</strong>
        </a>
    </li>
    <li style="<%= Authenticator.CheckRole(UserRole.Administrator) ? "": "display: none"%>">
        <a href="<%= Redirector.GetLink(ViewNames.Admin.NguoiSuDung_DanhSach) %>">
            <strong><span class="glyphicon glyphicon-bookmark" aria-hidden="true"></span>Quản trị hệ thống</strong>
        </a>
    </li>
    <li class="dropdown" style="<%= Authenticator.CheckLoggedIn() ? "": "display: none"%>">
        <a class="dropdown-toggle" data-toggle="dropdown" href="#">
            <i class="fa fa-user fa-fw"></i><strong><%= Sessions.DisplayName %></strong> <i class="fa fa-caret-down"></i>
        </a>
        <ul class="dropdown-menu dropdown-user">
            <li><a href="<%= Redirector.GetLink(ViewNames.Public.DoiMatKhau) %>"><i class="fa fa-terminal fa-fw"></i>Đổi mật khẩu</a>
            </li>
            <li class="divider"></li>
            <li><a href="<%= Redirector.GetLink(ViewNames.Public.DangXuat) %>"><i class="fa fa-sign-out fa-fw"></i>Thoát</a>
            </li>
        </ul>
    </li>
    <li style="<%= Authenticator.CheckLoggedIn() ? "display: none": ""%>">
        <a href="<%= Redirector.GetLink(ViewNames.Public.DangNhap) %>">
            <i class="fa fa-user" aria-hidden="true"></i>
            <strong>Đăng nhập</strong>
        </a>
    </li>
</ul>
<div class="navbar-header">
    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
        <span class="sr-only">Toggle navigation</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
    </button>
    <ul class="nav navbar-top-links navbar-left">
        <li>
            <a href="<%= Redirector.GetLink(ViewNames.Public.ThongKe_NhomDonVi) %>">
                <strong><span class="glyphicon glyphicon-home"></span>Trang chủ</strong>
            </a>
        </li>
    </ul>
</div>
