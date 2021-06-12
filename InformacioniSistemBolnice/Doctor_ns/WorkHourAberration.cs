using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public class WorkHourAberration
    {
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public WorkHourAberration(DateTime date, DateTime start, DateTime end)
        {
            Date = date;
            Start = start;
            End = end;
        }
    }
}
