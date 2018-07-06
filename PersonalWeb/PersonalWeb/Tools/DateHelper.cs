using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalWeb
{
    public class DateHelper
    {
        /// <summary>
        /// 获取当前月的天数
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int getDays(DateTime dateTime)
        {
            try
            {
                int year = dateTime.Year;
                int month = dateTime.Month;
                int days = DateTime.DaysInMonth(year, month);
                return days;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}