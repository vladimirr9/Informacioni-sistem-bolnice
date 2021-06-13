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
        private INotificationRepository _notificationRepository = new NotificationFileStorage();
        public void Create(Notification notification)
        {
            _notificationRepository.Add(notification);
        }

        public void Update(int iD, Notification initialNotification)
        {
            _notificationRepository.Update(iD, initialNotification);
        }
        public void Delete(Notification notification)
        {
            _notificationRepository.Remove(notification.ID);
        }

        public List<Notification> GetAll()
        {
            return _notificationRepository.GetAll();
        }

        public Notification GetOne(int iD)
        {
            return _notificationRepository.GetOne(iD);
        }
    }
}
