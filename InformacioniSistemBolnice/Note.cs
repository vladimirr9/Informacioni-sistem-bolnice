using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice
{
    public class Note
    {
        public String DescriptionOfNote { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartPeriodOfTime { get; set; }
        public DateTime EndPeriodOfTime { get; set; }
        public Boolean IsSetReminder { get; set; } = false;

        public Note()
        {
        }

        public Note(string descriptionOfNote, DateTime startDate, DateTime endDate, DateTime startPeriodOfTime, DateTime endPeriodOfTime)
        {
            DescriptionOfNote = descriptionOfNote;
            StartDate = startDate;
            EndDate = endDate;
            StartPeriodOfTime = startPeriodOfTime;
            EndPeriodOfTime = endPeriodOfTime;
        }
    }
}
