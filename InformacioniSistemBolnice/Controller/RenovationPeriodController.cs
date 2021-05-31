using InformacioniSistemBolnice.Manager_ns;
using InformacioniSistemBolnice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Controller
{
    public class RenovationPeriodController
    {
        RenovationPeriodService _renovationPeriodPeriodService = new RenovationPeriodService();

        public void AddRenovatoinPeriod(RenovationPeriod newRenovationPeriod)
        {
            _renovationPeriodPeriodService.AddRenovatoinPeriod(newRenovationPeriod);
        }
        public void UpdateRenovationPeriod(RenovationPeriod renovationPeriod)
        {
            _renovationPeriodPeriodService.UpdateRenovationPeriod(renovationPeriod);
        }
        public void RemoveRenovationPeriod(RenovationPeriod renovationPeriod)
        {
            _renovationPeriodPeriodService.RemoveRenovationPeriod(renovationPeriod);
        }
        public void ScheduleRenovation(Room room, DateTime startDate, DateTime endDate)
        {
            _renovationPeriodPeriodService.ScheduleRenovation(room, startDate, endDate);
        }

        public bool IsAvailable(Room room, DateTime start, DateTime end)
        {
            return _renovationPeriodPeriodService.IsAvailable(room, start, end);
        }
    }
}
