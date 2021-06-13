using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.User;
using Newtonsoft.Json;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface INotificationRepository
    {
        List<Notification> GetAll();

        Notification GetOne(int id);

        Boolean Remove(int id);

        Boolean Add(Notification newNotification);

        Boolean Update(int id, Notification newNotification);
    }
}
