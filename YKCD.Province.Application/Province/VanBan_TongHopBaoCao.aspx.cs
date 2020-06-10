using System;
using Framework.Helper;
using YKCD.Province.Application.Helper;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;
using GemBox.Document;
using GemBox.Document.Tables;
using System.Linq;

namespace YKCD.Province.Application.Province
{
    public partial class VanBan_TongHopBaoCao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ThongTinVanBanControl.DocumentID = Parameters.Id;
            
            if (!IsPostBack)
            {
                var document = DocumentServices.GetById(Parameters.Id);
                txtBaoCaoTongHop.Text = $@"<center><b>BÁO CÁO <br>
Tình hình thực hiện nội dung giao nhiệm vụ tại Công văn số { document.DocumentCode} <br>
ngày {document.ReleaseDate.ToDateString()} của UBND tỉnh.</b></center><br>
&nbsp;&nbsp;&nbsp;&nbsp;Ngày {document.ReleaseDate.ToDateString()}, UBND tỉnh đã ban hành Công văn số { document.DocumentCode} V/v: { document.MainContent} giao nhiệm vụ cho các đơn vị; qua tổng hợp, Văn phòng UBND tỉnh kính báo cáo UBND tỉnh tình hình thực hiện như sau:<br>
<table border='1' cellpadding='2' cellspacing='0' width='100%'>
    <tbody>
        <tr>
            <td><b> STT </b></td>
            <td><b> Nội dung giao nhiệm vụ</b></td>
     
                 <td><b> Ngày yêu cầu</b></td>
          
                      <td><b> Đơn vị theo dõi </b></td>
               
                           <td><b> Tình trạng </b></td>
                    
                                <td><b> Tình hình thực hiện </b></td>
                         
                                 </tr>";
                int totalRows;
                var listItems = RequestServices.GetList(MaVanBan: document.DocumentID);

                int stt = 1;

                if (listItems != null && listItems.Count > 0)
                {
                    foreach (Request item in listItems)
                    {
                        txtBaoCaoTongHop.Text += $@"<tr>                         
                                     <td>{stt}</td>                         
                                     <td> {item.RequestContent} </td>
                                        <td> {item.RequiredDate.ToDateString()} </td>                            
                                        <td> {item.PerformStatusString} </td>                            
                                        <td> {item.RequestStatusString} </td>                            
                                        <td> &nbsp;</td>                               
                                       </tr>";
                        stt++;
                    }
                }

                txtBaoCaoTongHop.Text += $@"</tbody></table><p align ='right'><b>CHUYÊN VIÊN THEO DÕI</b></p>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <br>
                                    <p align ='right'><b>{Sessions.User.FullName}</b></p>";
            }
            
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }
    }
}