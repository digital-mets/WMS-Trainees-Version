using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CapacityPlanning
    {
        public int RecordID { get; set; }
        public int Year { get; set; }
        public int WorkWeek { get; set; }
        public string SKUCode { get; set; }
        public int SequenceDay { get; set; }
        public int NoManpower { get; set; }
        public string AvailableMachine { get; set; }
        public string action { get; set; }
        public string filter { get; set; }
        public string Stages { get; set; }
        public string DActual { get; set; }
        public string AActual { get; set; }
    }
}
