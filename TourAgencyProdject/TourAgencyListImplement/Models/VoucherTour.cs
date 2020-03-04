using System;
using System.Collections.Generic;
using System.Text;

namespace TourAgencyListImplement.Models
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class VoucherTour
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int tourId { get; set; }
        public int Count { get; set; }
    }
}
