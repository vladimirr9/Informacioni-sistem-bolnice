using InformacioniSistemBolnice.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Service
{
    public class RenovationPeriodService
    {
        private RoomController _roomController = new RoomController();
        public void AddRenovatoinPeriod(RenovationPeriod newRenovationPeriod)
        {
            RenovationperiodFileRepository.AddRenovationPeriod(newRenovationPeriod);
        }

        public void UpdateRenovationPeriod(RenovationPeriod renovationPeriod)
        {
            RenovationperiodFileRepository.UpdateRenovationPeriod(renovationPeriod.Room.RoomId, renovationPeriod);
        }

        public void RemoveRenovationPeriod(RenovationPeriod renovationPeriod)
        {
            RenovationperiodFileRepository.RemoveRenovationPeriod(renovationPeriod.Room.RoomId);
        }

        public void ScheduleRenovation(Room room, DateTime startDate, DateTime endDate)
        {
            foreach(DateTime day in RenovationDays(startDate, endDate))
            {
                if(IsAvailable(room, startDate, endDate))
                {
                    if (DateTime.Today == day.Date)
                    {
                        room.IsActive = false;
                        RenovationPeriod renPer = new RenovationPeriod(startDate, endDate, false, room);
                        AddRenovatoinPeriod(renPer);
                        _roomController.UpdateRoom(room);
                    }
                } else
                {
                    MessageBox.Show("Ima zakazanih termina u tom periodu!", "Upozorenje", MessageBoxButton.OK);
                    return;
                }                
            }
        }

        public IEnumerable<DateTime> RenovationDays(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
                yield return day;
        }

        public bool IsAvailable(Room room, DateTime start, DateTime end) // proverava da li je RoomComboBox slobodna izmedju neka dva trenutka u vremenu
        {
            if (start.Equals(end))
                return true;
            bool retVal = true;
            foreach (Appointment appointment in AppointmentFileRepository.GetAll())
            {
                if (appointment.Room.Equals(room) && appointment.AppointmentStatus == AppointmentStatus.scheduled)
                {
                    if (start >= appointment.AppointmentDate && start <= appointment.AppointmentEnd)
                    {
                        retVal = false;
                        break;
                    }
                    if (end >= appointment.AppointmentDate && end <= appointment.AppointmentEnd)
                    {
                        retVal = false;
                        break;
                    }
                    if (start <= appointment.AppointmentDate && end >= appointment.AppointmentEnd)
                    {
                        retVal = false;
                        break;
                    }
                }
            }
        return retVal;
        }
    }
}
