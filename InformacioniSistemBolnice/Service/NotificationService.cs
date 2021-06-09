using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Service
{
    public class NotificationService
    {
        public void Create(Notification notification)
        {
            NotificationFileStorage.AddNotification(notification);
        }

        public void Update(int iD, Notification initialNotification)
        {
            NotificationFileStorage.UpdateNotification(iD, initialNotification);
        }
        public void Delete(Notification notification)
        {
            NotificationFileStorage.RemoveNotification(notification.ID);
        }

    }
}
