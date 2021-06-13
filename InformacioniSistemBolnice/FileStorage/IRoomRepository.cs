using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IRoomRepository
    {
        List<Room> GetAll();


        Room GetOne(int roomId);


        Boolean Remove(int roomId);


        Boolean Add(Room newRoom);


        Boolean Update(int roomId, Room newRoom);
        Room GetOneByName(String name);
    }
}
