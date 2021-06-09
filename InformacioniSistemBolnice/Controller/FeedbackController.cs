using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Service;
using InformacioniSistemBolnice.User;

namespace InformacioniSistemBolnice.Controller
{
    class FeedbackController
    {
        private FeedbackService _feedbackService = new FeedbackService();
        public void Add(Feedback feedback)
        {
            _feedbackService.Add(feedback);
        }

        public int GenerateId()
        {
            return _feedbackService.GenerateId();
        }
    }
}
