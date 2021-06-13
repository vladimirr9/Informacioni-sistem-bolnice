using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.Manager_ns;
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
        private RoomService _roomService = new RoomService();
        private IRenovationPeriodRepository _renovationPeriodRepository = new RenovationPeriodFileRepository();

        public void AddRenovatoinPeriod(RenovationPeriod newRenovationPeriod)
        {
            _renovationPeriodRepository.Add(newRenovationPeriod);
        }

        public void UpdateRenovationPeriod(RenovationPeriod renovationPeriod)
        {
            _renovationPeriodRepository.Update(renovationPeriod.Room.RoomId, renovationPeriod);
        }

        public void RemoveRenovationPeriod(RenovationPeriod renovationPeriod)
        {
            _renovationPeriodRepository.Remove(renovationPeriod.Room.RoomId);
        }

        public void ScheduleRenovation(Room room, DateTime startDate, DateTime endDate)
        {
            foreach (DateTime day in RenovationDays(startDate, endDate))
            {
                if (IsAvailable(room, startDate, endDate))
                {
                    if (DateTime.Today == day.Date)
                    {
                        CreateRenovationPeriod(room, startDate, endDate);
                    }
                    /*else if (DateTime.Today != day.Date)
                    {
                        CancelRenovation(CreateRenovationPeriod(room, startDate, endDate));
                    }*/
                }
                else
                {
                    MessageBox.Show("Ima zakazanih termina u tom periodu!", "Upozorenje", MessageBoxButton.OK);
                    return;
                }
            }
        }

        /*public void CancelRenovation(RenovationPeriod renovationPeriod)
        {
            renovationPeriod.Room.IsActive = true;
            renovationPeriod.IsDeleted = true;
            Update(renovationPeriod);
            _roomController.Update(renovationPeriod.Room);
        }*/

        private void CreateRenovationPeriod(Room room, DateTime startDate, DateTime endDate)
        {
            room.IsActive = false;
            RenovationPeriod renPer = new RenovationPeriod(startDate, endDate, false, room);
            AddRenovatoinPeriod(renPer);
            _roomService.UpdateRoom(room);

            //return renPer;
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
            AppointmentFileRepository _appointmentFileRepository = new AppointmentFileRepository();
            foreach (Appointment appointment in _appointmentFileRepository.GetAll())
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
