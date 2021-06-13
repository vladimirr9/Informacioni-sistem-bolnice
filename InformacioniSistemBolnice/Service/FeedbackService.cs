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
        private IFeedbackRepository _feedbackRepository = new FeedbackRepository();
        public void Add(Feedback feedback)
        {
            _feedbackRepository.Add(feedback);
        }

        public int GenerateId()
        {
            return _feedbackRepository.GetAll().Count + 1;
        }
    }
}
