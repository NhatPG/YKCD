using System;
using System.Linq;
using SmsStatisticForNamDong.SmsService;
using YKCD.Agency.Business.Services;
using Framework.Helper;
using YKCD.Agency.Business.Entities;

namespace SmsStatisticForNamDong
{
    class Program
    {
        static void Main(string[] args)
        {
            SendSms();
        }

        protected static void SendSms()
        {
            SmsBrandnameSoapClient smsClient = new SmsBrandnameSoapClient();

            foreach (var agency in DepartmentServices.GetList().Where(ag => !string.IsNullOrEmpty(ag.PhoneNumber?.Trim())))
            {
                var soLieu = RequestServices.LaySoLieuThongKe(MaDonVi: agency.DepartmentID);
                string smsContent = $"[YKCD] {agency.DepartmentName.ToUnsign()} co : {soLieu.NotPerform} ykcd chua thuc hien ({soLieu.NotPerformOutTerm} qua han); {soLieu.Performing} ykcd dang thuc hien ({soLieu.PerformingOutTerm} qua han).";

                foreach (var phoneNumber in agency.PhoneNumber.Split(';'))
                {
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        smsClient.GuiMotNoiDungNhieuSo(string.Empty, phoneNumber, smsContent, "ubnd_pdien", "Pd@3552374", 43);

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
