<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThongKeChuyenVien.aspx.cs" Inherits="YKCD.Agency.Application.Iframes.ThongKeChuyenVien" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .ykcd_links {
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
            color: #000000;
            text-align: left;
            text-decoration: none;
            padding: 0 3px 0 3px;
        }

            .ykcd_links:hover {
                font-family: Arial;
                font-size: 11px;
                font-weight: bold;
                color: red;
                text-align: left;
                text-decoration: none;
                padding: 0 3px 0 3px;
            }

        .ykcd_tieude {
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
            color: #FFFFFF;
            padding: 0 0 0 0px;
            text-align: center;
            background-color: #999999;
        }

        .ykcd_tieude_1 {
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
            color: #000000;
            padding: 0 0 0 0px;
            text-align: left;
            background-color: #CCCCCC;
            height: 20px;
        }

        .ykcd_tongcong {
            font-family: Arial;
            font-size: 11px;
            color: #FFCC00;
            padding: 0 0 0 0px;
            text-align: center;
            background-color: #999999;
            font-weight: bold;
        }

        .ykcdboldtxt {
            font-family: Arial;
            font-size: 15px;
            font-weight: bold;
            color: #853008;
        }

        .ykcd_normal_txt {
            font-family: Arial;
            font-size: 11px;
            font-weight: normal;
            color: #853008;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellpadding="2" cellspacing="1">
                <tbody>
                    <tr>
                        <td width="5%" rowspan="2" class="ykcd_tieude">Stt</td>
                        <td width="32%" rowspan="2" class="ykcd_tieude">Tên đơn vị/ cá nhân</td>
                        <td width="18%" colspan="2" height="20" class="ykcd_tieude"><strong>Chưa thực hiện </strong></td>
                        <td width="18%" colspan="2" class="ykcd_tieude"><strong>Đang thực hiện</strong></td>
                        <td width="27%" colspan="3" class="ykcd_tieude"><strong>Đã hoàn thành</strong></td>
                    </tr>
                    <tr>
                        <td width="9%" height="20" nowrap="nowrap" class="ykcd_tieude">Trong hạn</td>
                        <td width="9%" nowrap="nowrap" class="ykcd_tieude">Quá hạn</td>
                        <td width="9%" height="20" nowrap="nowrap" class="ykcd_tieude">Trong hạn</td>
                        <td width="9%" nowrap="nowrap" class="ykcd_tieude">Quá hạn</td>
                        <td width="9%" height="20" nowrap="nowrap" class="ykcd_tieude">Chờ xác nhận</td>
                        <td width="9%" height="20" nowrap="nowrap" class="ykcd_tieude">Trong hạn</td>
                        <td width="9%" nowrap="nowrap" class="ykcd_tieude">Quá hạn</td>
                    </tr>
                    <tr>
                        <td style="height: 20px;" colspan="9" class="ykcd_tieude_1">Thống kê theo người theo dõi</td>
                    </tr>

                    <% List<SoLieuThongKe> listItems = RequestServices.ThongKeTheoTatCaChuyenVienTheoDoi().Where(item => item.Total > 0).ToList();

                        SoLieuThongKe total = new SoLieuThongKe();

                        

                        total = new SoLieuThongKe();

                        for (int i = 0; i < listItems.Count; i++)
                        {
                            total.NotPerformInTerm += listItems[i].NotPerformInTerm;
                            total.NotPerformOutTerm += listItems[i].NotPerformOutTerm;
                            total.PerformingInTerm += listItems[i].PerformingInTerm;
                            total.PerformingOutTerm += listItems[i].PerformingOutTerm;
                            total.WaitToConfirm += listItems[i].WaitToConfirm;
                            total.DoneInTerm += listItems[i].DoneInTerm;
                            total.DoneOutTerm += listItems[i].DoneOutTerm;
                    %>
                    <tr bgcolor="#FFFFFF" class="ykcd_links">
                        <td height="20" align="center" valign="top"><%= i+1 %></td>
                        <td valign="top"><a class="ykcd_links" target="_blank" href="<%= Redirector.GetLink(ViewNames.Public.DanhSachYKCD, ParameterNames.StaffID, listItems[i].ObjectID) %>"><%= listItems[i].ObjectName %></a></td>
                        <td align="center">
                            <a target="_blank" class="ykcd_links" href="<%= Redirector.GetLink(ViewNames.Public.DanhSachYKCD, ParameterNames.StaffID, listItems[i].ObjectID, "status", TrangThai.ChuaThucHienTrongHan) %>"><%= (listItems[i].NotPerformInTerm != 0) ? listItems[i].NotPerformInTerm.ToString() : "-" %></a>
                        </td>
                        <td align="center">
                            <a target="_blank" class="ykcd_links" href="<%= Redirector.GetLink(ViewNames.Public.DanhSachYKCD, ParameterNames.StaffID, listItems[i].ObjectID, "status", TrangThai.ChuaThucHienQuaHan) %>"><%= (listItems[i].NotPerformOutTerm != 0) ? listItems[i].NotPerformOutTerm.ToString() : "-" %></a>
                        </td>
                        <td align="center"><a target="_blank" class="ykcd_links" href="<%= Redirector.GetLink(ViewNames.Public.DanhSachYKCD, ParameterNames.StaffID, listItems[i].ObjectID, "status", TrangThai.DangThucHienTrongHan) %>"><%= (listItems[i].PerformingInTerm != 0) ? listItems[i].PerformingInTerm.ToString() : "-" %></a>
                        </td>
                        <td align="center">
                            <a target="_blank" class="ykcd_links" href="<%= Redirector.GetLink(ViewNames.Public.DanhSachYKCD, ParameterNames.StaffID, listItems[i].ObjectID, "status", TrangThai.DangThucHienQuaHan) %>"><%= (listItems[i].PerformingOutTerm != 0) ? listItems[i].PerformingOutTerm.ToString() : "-" %></a>
                        </td>
                        <td align="center">
                            <a target="_blank" class="ykcd_links" href="<%= Redirector.GetLink(ViewNames.Public.DanhSachYKCD, ParameterNames.StaffID, listItems[i].ObjectID, "status", TrangThai.ChoXacNhan) %>"><%= (listItems[i].WaitToConfirm != 0) ? listItems[i].WaitToConfirm.ToString() : "-" %></a>
                        </td>
                        <td align="center">
                            <a target="_blank" class="ykcd_links" href="<%= Redirector.GetLink(ViewNames.Public.DanhSachYKCD, ParameterNames.StaffID, listItems[i].ObjectID, "status", TrangThai.DaThucHienDungHan) %>"><%= (listItems[i].DoneInTerm != 0) ? listItems[i].DoneInTerm.ToString() : "-" %></a>
                        </td>
                        <td align="center">
                            <a target="_blank" class="ykcd_links" href="<%= Redirector.GetLink(ViewNames.Public.DanhSachYKCD, ParameterNames.StaffID, listItems[i].ObjectID, "status", TrangThai.DaThucHienTreHan) %>"><%= (listItems[i].DoneOutTerm != 0) ? listItems[i].DoneOutTerm.ToString() : "-" %></a>
                        </td>
                    </tr>
                    <%
                        }
                    %>
                    <tr>
                        <td height="25" colspan="2" class="ykcd_tongcong">Tổng cộng</td>
                        <td class="ykcd_tongcong"><%= total.NotPerformInTerm %></td>
                        <td class="ykcd_tongcong"><%= total.NotPerformOutTerm %></td>
                        <td class="ykcd_tongcong"><%= total.PerformingInTerm %></td>
                        <td class="ykcd_tongcong"><%= total.PerformingOutTerm %></td>
                        <td class="ykcd_tongcong"><%= total.WaitToConfirm %></td>
                        <td class="ykcd_tongcong"><%= total.DoneInTerm %></td>
                        <td class="ykcd_tongcong"><%= total.DoneOutTerm %></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>