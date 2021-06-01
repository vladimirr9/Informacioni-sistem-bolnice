using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InformacioniSistemBolnice.Commands
{
    public static class RoutedCommands
    {
        
        public static RoutedUICommand StartingPage = new RoutedUICommand(
            "Starting Page",
            "StartingPage",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F1, ModifierKeys.None),
            }
            );
        public static RoutedUICommand Patients = new RoutedUICommand(
            "Patients",
            "Patients",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F2, ModifierKeys.None),
            }
            );
        public static RoutedUICommand Doctors = new RoutedUICommand(
            "Doctors",
            "Doctors",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F3, ModifierKeys.None),
            }
            );
        public static RoutedUICommand Appointments = new RoutedUICommand(
            "Appointments",
            "Appointments",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F4, ModifierKeys.None),
            }
            );
        public static RoutedUICommand Reports = new RoutedUICommand(
            "Reports",
            "Reports",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F5, ModifierKeys.None),
            }
            );


    }
}
