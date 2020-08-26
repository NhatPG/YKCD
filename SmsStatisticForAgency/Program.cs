using Framework.Helper;
using SmsStatisticForAgency.SmsService;
using System;
using System.Linq;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace SmsStatisticForAgency
{
    class Program
    {
        static void Main(string[] args)
        {
            SmsBrandnameSoapClient smsClient = new SmsBrandnameSoapClient();
            smsClient.GuiMotNoiDungNhieuSo(string.Empty, "0935669664", "[HSCV]Nhat test tin nhan", "ubndtth", "1682e86c", 8);
            ///SendSms();
            /////Test abc11111
            return;
        }

        protected static void SendSms()
        {
            SmsBrandnameSoapClient smsClient = new SmsBrandnameSoapClient();

            foreach (var agency in AgencyServices.GetList().Where(ag => !string.IsNullOrEmpty(ag.PhoneNumber?.Trim())))
            {
                var soLieu = RequestServices.LaySoLieuThongKe(MaDonVi: agency.AgencyID);
                string smsContent = $"[YKCD] {agency.AgencyName.ToUnsign()} co : {soLieu.NotPerform} ykcd chua thuc hien ({soLieu.NotPerformOutTerm} qua han); {soLieu.Performing} ykcd dang thuc hien ({soLieu.PerformingOutTerm} qua han).";

                foreach (var phoneNumber in agency.PhoneNumber.Split(';'))
                {
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        smsClient.GuiMotNoiDungNhieuSo(string.Empty, phoneNumber, smsContent, "ubndtth", "1682e86c", 8);

                        SmsServices.Create(new Sms
                        {
                            ReceiverNumber = phoneNumber,
                            SmsContent = smsContent,
                            SendTime = DateTime.Now
                        });
                    }
                }
            }
        }
    }
}
