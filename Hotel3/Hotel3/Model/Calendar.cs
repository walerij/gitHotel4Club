using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace Hotel3.Model
{
    public class Calendar
    {
        private MySQL sql;

        public Calendar(MySQL sql)
        {
            this.sql = sql;
        }

        /// <summary>
        /// добавление дней
        /// </summary>
        /// <param name="year">год</param>
        public void InsertDays(int year)
        {
            DateTime day = new DateTime(year,1,1);

         while(day.Year==year)
            {
                int wend = 0;
                if (day.DayOfWeek==DayOfWeek.Friday ||
                    day.DayOfWeek == DayOfWeek.Saturday ||
                    day.DayOfWeek == DayOfWeek.Sunday)
                    wend = 1;
                string query=@" INSERT IGNORE INTO calendar SET day = '"+day.ToString("yyyy-MM-dd")+@"', wend = '"+wend+@"', holiday = '1' ";
                do this.sql.Insert(query);
                while (sql.SqlError());

                day=day.AddDays(1);
            }
        }

    }
}
