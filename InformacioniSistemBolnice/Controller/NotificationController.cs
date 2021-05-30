using InformacioniSistemBolnice.User;
using InformacioniSistemBolnice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Controller
{
    public class NotificationController
    {
        private NotificationService _notificationService = new NotificationService();

        public void Create(Notification notification)
        {
            _notificationService.Create(notification);
        }

        public void Update(int iD, Notification initialNotification)
        {
            _notificationService.Update(iD, initialNotification);
        }
        public void Delete(Notification notification)
        {
            _notificationService.Delete(notification);
        }
    }
}
