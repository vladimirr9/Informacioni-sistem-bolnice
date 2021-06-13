﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IAnamnesisRepository
    {
        List<Anamnesis> GetAll();
        Boolean Add(Anamnesis newAnamnesis);
        Boolean Update(int idOfAnamnesis, Anamnesis newAnamnesis);

    }
}
