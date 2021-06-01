using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Secretary_ns
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        private static AccountPage _instance;
        public static AccountPage GetPage(SecretaryMain parent)
        {
            if (_instance == null)
                _instance = new AccountPage();
            parent.Title.Content = "Nalog";
            return _instance;
        }
        private AccountPage()
        {
            InitializeComponent();
        }
    }
}
