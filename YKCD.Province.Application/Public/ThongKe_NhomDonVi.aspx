<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Public.Master" AutoEventWireup="true" CodeBehind="ThongKe_NhomDonVi.aspx.cs" 
    Inherits="YKCD.Province.Application.Public.ThongKe_NhomDonVi" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <fieldset>
        <legend>TÌNH HÌNH THỰC HIỆN Ý KIẾN CHỈ ĐẠO Ở CÁC ĐƠN VỊ </legend>
    </fieldset>
    <asp:ListView ID="lvData" runat="server" ViewStateMode="Disabled" ItemType="YKCD.Province.Business.Components.SoLieuThongKe">
        <LayoutTemplate>
            <table class="table table-bordered table-striped table-hover">
                <thead>
                    <tr>
                        <th rowspan="2" scope="col" class="text-center">STT</th>
                        <th rowspan="2" scope="col">Đơn vị</th>
                        <th colspan="2" class="text-center">Chưa thực hiện</th>
                        <th colspan="2" class="text-center">Đang thực hiện</th>
                        <th colspan="3" class="text-center">Đã hoàn thành</th>
                    </tr>
                    <tr>
                        <th scope="col">Chưa đến hạn</th>
                        <th scope="col">Đã quá hạn</th>
                        <th scope="col">Chưa đến hạn</th>
                        <th scope="col">Đã quá hạn</th>
                        <th scope="col">Chờ xác nhận</th>
                        <th scope="col">Đúng hạn</th>
                        <th scope="col">Quá hạn</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                    <tr class="total-row" runat="server">
                        <td class="text-center" colspan="2">
                            <strong>Tổng cộng</strong>
                        </td>
                        <td class="text-in-term"><%= tongCong.NotPerformInTerm > 0 ? tongCong.NotPerformInTerm.ToString() : "-" %>
                        </td>
                        <td class="text-out-term"><%= tongCong.NotPerformOutTerm > 0 ? tongCong.NotPerformOutTerm.ToString() : "-" %>
                        </td>
                        <td class="text-in-term"><%= tongCong.PerformingInTerm > 0 ? tongCong.PerformingInTerm.ToString() : "-" %>
                        </td>
                        <td class="text-out-term"><%= tongCong.PerformingOutTerm > 0 ? tongCong.PerformingOutTerm.ToString() : "-" %>
                        </td>
                        <td class="text-out-term"><%= tongCong.WaitToConfirm > 0 ? tongCong.WaitToConfirm.ToString() : "-" %>
                        </td>
                        <td class="text-in-term"><%= tongCong.DoneInTerm > 0 ? tongCong.DoneInTerm.ToString() : "-" %>
                        </td>
                        <td class="text-out-term"><%= tongCong.DoneOutTerm > 0 ? tongCong.DoneOutTerm.ToString() : "-" %>
                        </td>
                    </tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="text-center"><%# Container.DataItemIndex + 1%>
                </td>
                <td class="text-left">
                    <strong>
                        <a href="<%# Redirector.GetLink(ViewNames.Public.ThongKe_DonVi, "id", Item.ObjectID) %>"><%# Item.ObjectName %></a>
                    </strong>
                </td>
                <td class="text-in-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Public.ThongKe_DonVi, "id", Item.ObjectID) %>">
                        <%# Item.NotPerformInTerm > 0 ? Item.NotPerformInTerm.ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Public.ThongKe_DonVi, "id", Eval("ObjectID")) %>">
                        <%# Eval("NotPerformOutTerm").ToInteger() > 0 ? Eval("NotPerformOutTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-in-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Public.ThongKe_DonVi, "id", Eval("ObjectID")) %>">
                        <%# Eval("PerformingInTerm").ToInteger() > 0 ? Eval("PerformingInTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Public.ThongKe_DonVi, "id", Eval("ObjectID")) %>">
                        <%# Eval("PerformingOutTerm").ToInteger() > 0 ? Eval("PerformingOutTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Public.ThongKe_DonVi, "id", Eval("ObjectID")) %>">
                        <%# Eval("WaitToConfirm").ToInteger() > 0 ? Eval("WaitToConfirm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-in-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Public.ThongKe_DonVi, "id", Eval("ObjectID")) %>">
                        <%# Eval("DoneInTerm").ToInteger() > 0 ? Eval("DoneInTerm").ToString() : "-" %>
                    </a>
                </td>
                <td class="text-out-term">
                    <a href="<%# Redirector.GetLink(ViewNames.Public.ThongKe_DonVi, "id", Eval("ObjectID")) %>">
                        <%# Eval("DoneOutTerm").ToInteger() > 0 ? Eval("DoneOutTerm").ToString() : "-" %>
                    </a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <fieldset>
        <legend>THỐNG KÊ SỐ Ý KIẾN CHỈ ĐẠO CHƯA HOÀN THÀNH Ở CÁC ĐƠN VỊ </legend>
    </fieldset>
    <div>
        <asp:Literal ID="ltrChart" runat="server" ViewStateMode="Disabled"></asp:Literal>
    </div>
</asp:Content>
