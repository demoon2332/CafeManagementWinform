using CafeManagement.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.DAO
{
    public class SeatDAO
    {
        private static SeatDAO instance;

        public static SeatDAO Instance
        {
            get { if (instance == null) instance = new SeatDAO(); return  SeatDAO.instance; }
            private set { instance = value; }
        }

        public static int SeatWidth = 80;
        public static int SeatHeight = 80;

        private SeatDAO() { }

        public List<Seat> LoadSeatList()
        {
            List<Seat> seatList = new List<Seat>();

            DataTable data = DataProvider.Instance.ExecuteQuery("getSeatList");

            foreach(DataRow item in data.Rows)
            {
                Seat seat = new Seat(item);
                seatList.Add(seat);
            }

            return seatList;
        }

    }
}
