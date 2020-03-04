using System;
using System.Collections.Generic;
using System.Text;
using TourAgencyListImplement.Models;

namespace TourAgencyListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Tour> tours { get; set; }
        public List<Order> Orders { get; set; }
        public List<Voucher> Products { get; set; }
        public List<VoucherTour> Producttours { get; set; }
        private DataListSingleton()
        {
            
            tours = new List<Tour>();
            Orders = new List<Order>();
            Products = new List<Voucher>();
            Producttours = new List<VoucherTour>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
