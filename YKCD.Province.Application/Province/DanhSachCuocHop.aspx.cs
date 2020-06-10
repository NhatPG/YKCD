using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Helper;
using Framework.Web;
using YKCD.Province.Business.Entities;
using YKCD.Province.Business.Services;

namespace YKCD.Province.Application.Province
{
    public partial class DanhSachCuocHop : ListViewPageBase
    {
        public static List<DateTime> holidays = ToDateList(HolidayServices.GetList());

        protected override void SetValueForBaseControls()
        {
            BaseListView = lvData;
            BasePager = Pager;
        }

        protected override void GetDataList()
        {
            if ("uid".ToUrlInteger() > 0)
            {
                if ("type".ToUrlString().Equals("nodoc"))
                {
                    switch ("status".ToUrlString())
                    {
                        case "late":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => item.StaffID == "uid".ToUrlInteger() || item.PresidentID == "uid".ToUrlInteger())
                                ?.Where(item => (item.DocumentID <= 0 && GetBusinessDays(item.Time, DateTime.Now) > 3)).ToList();
                            break;
                        case "ontime":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => item.StaffID == "uid".ToUrlInteger() || item.PresidentID == "uid".ToUrlInteger())
                                ?.Where(item => (item.DocumentID <= 0 && GetBusinessDays(item.Time, DateTime.Now) <= 3)).ToList();
                            break;
                        default:
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => item.StaffID == "uid".ToUrlInteger() || item.PresidentID == "uid".ToUrlInteger())
                                ?.Where(item=> item.DocumentID <= 0).ToList();
                            break;
                    }
                }
                else if ("type".ToUrlString().Equals("havedoc"))
                {
                    switch ("status".ToUrlString())
                    {
                        case "late":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => item.StaffID == "uid".ToUrlInteger() || item.PresidentID == "uid".ToUrlInteger())
                                ?.Where(item => (item.DocumentID > 0 && GetBusinessDays(item.Time, item.Document.ReleaseDate) > 3)).ToList();
                            break;
                        case "ontime":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => item.StaffID == "uid".ToUrlInteger() || item.PresidentID == "uid".ToUrlInteger())
                                ?.Where(item => (item.DocumentID > 0 && GetBusinessDays(item.Time, item.Document.ReleaseDate) <= 3)).ToList();
                            break;
                        default:
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => item.StaffID == "uid".ToUrlInteger() || item.PresidentID == "uid".ToUrlInteger())
                                ?.Where(item => item.DocumentID > 0).ToList();
                            break;
                    }
                }
                else
                {
                    switch ("status".ToUrlString())
                    {
                        case "late":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => item.StaffID == "uid".ToUrlInteger() || item.PresidentID == "uid".ToUrlInteger())
                                ?.Where(item => (item.DocumentID <= 0 && GetBusinessDays(item.Time, DateTime.Now) > 3) || (item.DocumentID > 0 && GetBusinessDays(item.Time, item.Document.ReleaseDate) > 3)).ToList();
                            break;
                        case "ontime":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => item.StaffID == "uid".ToUrlInteger() || item.PresidentID == "uid".ToUrlInteger())
                                ?.Where(item => (item.DocumentID <= 0 && GetBusinessDays(item.Time, DateTime.Now) <= 3) || (item.DocumentID > 0 && GetBusinessDays(item.Time, item.Document.ReleaseDate) <= 3)).ToList();
                            break;
                        default:
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => item.StaffID == "uid".ToUrlInteger() || item.PresidentID == "uid".ToUrlInteger()).ToList();
                            break;
                    }
                }
            }
            else
            {
                if ("type".ToUrlString().Equals("nodoc"))
                {
                    switch ("status".ToUrlString())
                    {
                        case "late":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => (item.DocumentID <= 0 && GetBusinessDays(item.Time, DateTime.Now) > 3)).ToList();
                            break;
                        case "ontime":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => (item.DocumentID <= 0 && GetBusinessDays(item.Time, DateTime.Now) <= 3)).ToList();
                            break;
                        default:
                            BaseCollection = MettingServices.GetList();
                            break;
                    }
                }
                else if ("type".ToUrlString().Equals("havedoc"))
                {
                    switch ("status".ToUrlString())
                    {
                        case "late":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => (item.DocumentID > 0 && GetBusinessDays(item.Time, item.Document.ReleaseDate) > 3)).ToList();
                            break;
                        case "ontime":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => (item.DocumentID > 0 && GetBusinessDays(item.Time, item.Document.ReleaseDate) <= 3)).ToList();
                            break;
                        default:
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => item.DocumentID > 0).ToList();
                            break;
                    }
                }
                else
                {
                    switch ("status".ToUrlString())
                    {
                        case "late":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => (item.DocumentID <= 0 && GetBusinessDays(item.Time, DateTime.Now) > 3) || (item.DocumentID > 0 && GetBusinessDays(item.Time, item.Document.ReleaseDate) > 3)).ToList();
                            break;
                        case "ontime":
                            BaseCollection = MettingServices.GetList()
                                ?.Where(item => (item.DocumentID <= 0 && GetBusinessDays(item.Time, DateTime.Now) <= 3) || (item.DocumentID > 0 && GetBusinessDays(item.Time, item.Document.ReleaseDate) <= 3)).ToList();
                            break;
                        default:
                            BaseCollection = MettingServices.GetList();
                            break;
                    }
                }
            }
        }

        public string GetColor(DateTime mettingTime, int documentID)
        {
            if (documentID <= 0)
            {
                if (HolidayServices.GetBusinessDays(mettingTime, DateTime.Now) > 3)
                    return "text-out-term";
                else
                    return "";
            }
            else
            {
                Document document = DocumentServices.GetById(documentID);
                
                if (HolidayServices.GetBusinessDays(mettingTime, document.ReleaseDate) > 3)
                    return "text-out-term";
                else
                    return "";
            }
        }

        public static double GetBusinessDays(DateTime startD, DateTime endD)
        {
            int result = 0;

            DateTime check = startD.Date;

            while (check < endD.Date)
            {
                if (check.DayOfWeek != DayOfWeek.Saturday && check.DayOfWeek != DayOfWeek.Sunday &&
                    !holidays.Contains(check))
                {
                    result++;
                }

                check = check.AddDays(1);
            }

            return result;
        }

        public static List<DateTime> ToDateList(List<Holiday> holidayList)
        {
            List<DateTime> result = new List<DateTime>();

            foreach (var holiday in holidayList)
            {
                DateTime check = holiday.FromDate;

                while (check <= holiday.ToDate)
                {
                    if (!result.Contains(check))
                    {
                        result.Add(check.Date);
                        check = check.AddDays(1);
                    }
                }
            }

            return result;
        }

        protected void btnThongKe_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnDownLoad_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}