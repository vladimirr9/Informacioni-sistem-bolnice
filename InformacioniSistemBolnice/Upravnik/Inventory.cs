using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Inventory
    {
        private String _inventoryId;
        private String _name;
        private InventoryType _inventoryType;
        private int _quantity;
        private Boolean _isDeleted;
        private int _roomId;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String Id
        {
            get { return _inventoryId; }
            set { _inventoryId = value; }
        }

        public InventoryType InventoryType
        {
            get { return _inventoryType; }
            set { _inventoryType = value; }
        }
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public Boolean IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        public int RoomId
        {
            get { return _roomId; }
            set { _roomId = value; }
        }

        public Inventory() { }

        public Inventory(int roomId, String id, String name, InventoryType type, int quantity, Boolean isDeleted)
        {
            RoomId = roomId;
            Id = id;
            Name = name;
            InventoryType = type;
            Quantity = quantity;
            IsDeleted = isDeleted;
        }

        /*public Oprema(string sifra, string Name, TipOpreme tipOpreme, int kolicina, bool IsDeleted)
        {
            this.sifra = sifra;
            this.Name = Name;
            this.tipOpreme = tipOpreme;
            this.kolicina = kolicina;
            this.IsDeleted = IsDeleted;
        }*/
    }
