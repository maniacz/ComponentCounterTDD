using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentCounter
{
    class ViewController
    {
        private MainForm mainForm;
        DBHelper db = new DBHelper(Line.EPS_FIAT);

        public ViewController(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        internal DataView FillDataGridView(DateTime dateFrom, DateTime timeFrom, DateTime dateTo, DateTime timeTo, int lineIndex)
        {
            DateTime dateTimeFrom = dateFrom.Date + timeFrom.TimeOfDay;
            DateTime dateTimeTo = dateTo.Date + timeTo.TimeOfDay;

            //TODO: To chyba można przerobić zgodnie z wzorcem Factory
            db = new DBHelper((Line)lineIndex);
            return db.GetDrawerCountData(dateTimeFrom, dateTimeTo).Tables[0].DefaultView;
        }

        internal object[] GetLineNames()
        {
            object[] lines = new object[Enum.GetValues(typeof(Line)).Length];
            int i = 0;

            foreach (var line in Enum.GetValues(typeof(Line)))
            {
                lines[i++] = line;
            }

            return lines;
        }
    }
}
