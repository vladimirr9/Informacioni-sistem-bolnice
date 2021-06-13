using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IUserRepository<Entity> where Entity : global::User
    {
        List<Entity> GetAll();
        Entity GetOne(string username);
        Boolean Remove(string username);
        Boolean Add(Entity newEntity);
        Boolean Update(string username, Entity newEntity);

    }
}
