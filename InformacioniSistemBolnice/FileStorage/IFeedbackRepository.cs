using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.User;

namespace InformacioniSistemBolnice.FileStorage
{
    public interface IFeedbackRepository
    {
        List<Feedback> GetAll();
        Boolean Add(Feedback newfFeedback);
    }
}
