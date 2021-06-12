using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.Upravnik;

namespace InformacioniSistemBolnice.Factory
{
    class ManagerLoginer : Loginer
    {
        private Manager _manager;
        public ManagerLoginer(Manager manager)
        {
            this._manager = manager;
        }

        public override void Login()
        {
            UpravnikWindow managerWindow = new UpravnikWindow(_manager);
            Application.Current.MainWindow = managerWindow;
            managerWindow.Show();
        }
    }
}
