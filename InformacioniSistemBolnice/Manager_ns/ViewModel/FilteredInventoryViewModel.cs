using InformacioniSistemBolnice.Controller;
using InformacioniSistemBolnice.Upravnik;
using InformacioniSistemBolnice.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InformacioniSistemBolnice.Manager_ns.ViewModel
{
    class FilteredInventoryViewModel : BindableBase
    {
        private WindowProstorije _parent;
        private String _search;
        private RoomController _roomController = new RoomController();
        private ItemCollection inventories;
        private MyICommand CancelCommand { get; set; }
        public FilteredInventoryViewModel(WindowProstorije wp, String search)
        {
            _parent = wp;
            _search = search;
            CancelCommand = new MyICommand(Close);
            UpdateTable();
        }
        private void UpdateTable()
        {
            _roomController.FilteredInventory(Inventories, _search);
        }

        private void Close()
        {
            this.Close();
        }
        public ItemCollection Inventories
        {
            get { return inventories; }
            set
            {
                inventories = value;
                OnPropertyChanged("Inventories");
            }
        }
    }

}
