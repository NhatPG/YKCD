using Framework.Helper;
using SmsStatisticForYTe.SmsService;
using System;
using System.Linq;
using YKCD.Agency.Business.Components;
using YKCD.Agency.Business.Entities;
using YKCD.Agency.Business.Services;

namespace SmsStatisticForYTe
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        protected static void SendSms()
        {
            SmsBrandnameSoapClient smsClient = new SmsBrandnameSoapClient();

            foreach (var agency in DepartmentServices.GetList().Where(ag => !string.IsNullOrEmpty(ag.PhoneNumber?.Trim())))
            {
                var dsYkcd = RequestServices.GetList(MaDonViThucHien: agency.DepartmentID, TrangThai: TrangThai.ChuaThucHien).Where(item => item.RequiredDate.AddDays(-2) >= DateTime.Now.Date);

                foreach (var ykcd in dsYkcd)
                {
                    string smsContent = $"[YKCD] {agency.DepartmentName.ToUnsign()} co van ban so {ykcd.DocumentCode} chua bao cao. Thoi han: {ykcd.RequiredDate.ToDateString()}.";

                    foreach (var phoneNumber in agency.PhoneNumber.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(phoneNumber))
                        {
                            smsClient.GuiMotNoiDungNhieuSo(string.Empty, phoneNumber, smsContent, "syt_tthue", "u2AuWJ7ZNNTR8ge2v7b4CQ", 18);

                            SmsServices.Create(new Sms
                            {
                                ReceiverNumber = phoneNumber,
                                SmsContent = smsContent,
                                SendTime = DateTime.Now
                            });
                        }
                    }
                }

                dsYkcd = RequestServices.GetList(MaDonViThucHien: agency.DepartmentID, TrangThai: TrangThai.DangThucHien).Where(item => item.RequiredDate.AddDays(-2) >= DateTime.Now.Date);

                foreach (var ykcd in dsYkcd)
                {
                    string smsContent = $"[YKCD] {agency.DepartmentName.ToUnsign()} co van ban so {ykcd.DocumentCode} chua bao cao. Thoi han: {ykcd.RequiredDate.ToDateString()}.";

                    foreach (var phoneNumber in agency.PhoneNumber.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(phoneNumber))
                        {
                            smsClient.GuiMotNoiDungNhieuSo(string.Empty, phoneNumber, smsContent, "syt_tthue", "u2AuWJ7ZNNTR8ge2v7b4CQ", 18);

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
}
