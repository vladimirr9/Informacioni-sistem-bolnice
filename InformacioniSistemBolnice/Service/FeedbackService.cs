using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.FileStorage;
using InformacioniSistemBolnice.User;

namespace InformacioniSistemBolnice.Service
{
    class FeedbackService
    {
        public void Add(Feedback feedback)
        {
            FeedbackRepository.Add(feedback);
        }

        public int GenerateId()
        {
            return FeedbackRepository.GetAll().Count + 1;
        }
    }
}
