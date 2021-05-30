using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RenovationPeriod
{
        private DateTime _startDate;
        private DateTime _endDate;
        private Boolean _isDeleted;
        private Room _room;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public Boolean IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        public Room Room
        {
            get { return _room; }
            set { _room = value; }
        }

        public RenovationPeriod() { }

        public RenovationPeriod(DateTime start, DateTime end, Boolean isDeleted, Room room)
        {
            StartDate = start;
            EndDate = end;
            IsDeleted = isDeleted;
            Room = room;
        }
}

