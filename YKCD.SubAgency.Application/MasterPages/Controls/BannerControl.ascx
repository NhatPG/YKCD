<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerControl.ascx.cs" Inherits="YKCD.SubAgency.Application.MasterPages.Controls.BannerControl" %>
<div class="row hidden-xs">
    <div class="col-md-12" style="background-color: #b22222;">
        <table style="width: 100%">
            <tbody>
            <tr>
                <td style="width: 200px; text-align: center; padding: 5px">
                    <img class="banner-logo" src="<%=Page.ResolveClientUrl("~/Images/quochuy.png")%>" alt="" />
                </td>
                <td style="text-align: left">
                    <p style="font-size: 17px; color: #fff; font-weight: bold"><%= AppSettings.SUBAGENCY_NAME.ToUpper() %></p>
                    <p style="font-size: 27px; color: #fff; font-weight: bold">THEO DÕI Ý KIẾN CHỈ ĐẠO VÀ VĂN BẢN BAN HÀNH</p>
                </td>
            </tr>
            <tr>
                <td style="line-height: 5px; background-color: #f09f1a;" colspan="2">&nbsp;</td>
            </tr>
            </tbody>
        </table>
    </div>
</div>
