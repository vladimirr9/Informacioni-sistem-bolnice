﻿using System;
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

namespace InformacioniSistemBolnice.Upravnik
{
    /// <summary>
    /// Interaction logic for FiltriranaOprema.xaml
    /// </summary>
    public partial class FiltriranaOprema : Window
    {
        private WindowProstorije parent;
        private String pretraga;
        public FiltriranaOprema(WindowProstorije parent, String search)
        {
            InitializeComponent();
            this.parent = parent;
            this.pretraga = search;
            //UpdateTable();

            List<Room> sveProstorije = RoomFileRepository.GetAll();
            //List<Oprema> opremaLista = new List<Oprema>();

            foreach (Room p in sveProstorije)
            {
                foreach (Inventory o in p.InventoryList)
                {
                    if (o.Name.Equals(pretraga))
                    {
                        dataGridOprema.Items.Add(o);
                    }
                }
            }

            this.DataContext = this;
        }

        private void Zatvori(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}