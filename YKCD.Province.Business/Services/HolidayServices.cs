using System;
using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Services
{
    public class HolidayServices : BaseService<Holiday>
    {
        public static List<DateTime> holidays = ToDateList(GetList());

        public static List<Holiday> GetList(string searchText = "", string connectionName = "DatabaseConnection")
        {
            using (var db = new Database(connectionName))
            {
                return db.Fetch<Holiday>("WHERE [IsDeleted] = 0")
                    .Where(item => string.IsNullOrEmpty(searchText) || item.HolidayName.Contains(searchText))
                    .OrderBy(item => item.FromDate)
                    .ToList();
            }
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


    }
}