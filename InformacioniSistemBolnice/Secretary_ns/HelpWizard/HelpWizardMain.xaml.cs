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
using System.Windows.Shapes;

namespace InformacioniSistemBolnice.Secretary_ns.HelpWizard
{
    /// <summary>
    /// Interaction logic for HelpWizardMain.xaml
    /// </summary>
    public partial class HelpWizardMain : Window
    {
        private int _currentPageIndex = 0;
        private List<Page> _pages;
        private Secretary _secretary;
        public HelpWizardMain(Secretary secretary)
        {
            _secretary = secretary;
            _pages = new List<Page>();
            InitializeComponent();
            _pages.Add(new Page1());
            _pages.Add(new Page2());
            _pages.Add(new Page3());
            _pages.Add(new Page4());
            _pages.Add(new Page5());
            _pages.Add(new Page6());
            Main.Content = _pages[0];
        }

        private void Back_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _currentPageIndex--;
            Main.Content = _pages[_currentPageIndex];
        }

        private void Back_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _currentPageIndex > 0;
        }
        private void Next_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _currentPageIndex++;
            Main.Content = _pages[_currentPageIndex];
        }

        private void Next_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _currentPageIndex < _pages.Count - 1;
        }
        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Finish_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _secretary.FirstLogin = false;
            SecretaryFileRepository secretaryFileRepository = new SecretaryFileRepository();
            secretaryFileRepository.UpdateSecretary(_secretary.Username, _secretary);
            Close();
        }

        private void Finish_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _currentPageIndex == _pages.Count - 1;
        }
    }
}
