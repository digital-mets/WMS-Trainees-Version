using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ManningSchedule
    {
        public string RecordID { get; set; }
        public int Year { get; set; }
        public int WorkWeek { get; set; }
        public string DayNo { get; set; }
        public string Action { get; set; }
        public string Filter { get; set; }
        public string Userid { get; set; }
        public string Generate { get; set; }
        public string Shift { get; set; }
        public string Value { get; set; }
        public string Position { get; set; }
        public string DirectPlan { get; set; }
        public string DirectActual { get; set; }
        public string DirectPercent { get; set; }
        public string AgencyPlan { get; set; }
        public string AgencyActual { get; set; }
        public string AgencyPercent { get; set; }
    }
}
