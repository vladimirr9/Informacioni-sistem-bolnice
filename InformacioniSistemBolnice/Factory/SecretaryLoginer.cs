using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.Secretary_ns;
using InformacioniSistemBolnice.Secretary_ns.HelpWizard;

namespace InformacioniSistemBolnice.Factory
{
    class SecretaryLoginer : Loginer
    {
        private Secretary _secretary;
        public SecretaryLoginer(Secretary secretary)
        {
            this._secretary = secretary;
        }

        public override void Login()
        {
            if (_secretary.FirstLogin)
            {
                HelpWizardMain helpWizardMain = new HelpWizardMain(_secretary);
                helpWizardMain.ShowDialog();
            }
            SecretaryMain secretaryMain = new SecretaryMain(_secretary);
            Application.Current.MainWindow = secretaryMain;
            secretaryMain.Main.Content = StartingPage.GetPage(_secretary, secretaryMain);
            secretaryMain.Show();
        }
    }
}
