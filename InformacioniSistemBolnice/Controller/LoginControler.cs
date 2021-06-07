using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.Secretary_ns;
using InformacioniSistemBolnice.Service;

namespace InformacioniSistemBolnice.Controller
{
    class LoginControler
    {
        private LoginService _loginService = new LoginService();
        public Patient FindPatient(String username, String password)
        {
            return _loginService.FinPatient(username, password);
        }

        public Secretary FindSecretary(String username, String password)
        {
            return _loginService.FindSecretary(username, password);
        }

        public Doctor FindDoctor(String username, String password)
        {
            return _loginService.FindDoctor(username, password);
        }

        public Manager FindManager(String username, String password)
        {
            return _loginService.FindManager(username, password);
        }
    }
}
