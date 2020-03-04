using System;
using System.Collections.Generic;
using System.Text;

namespace TourAgencyBusinessLogic.BindingModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class VoucherBindingModel
    {
        public int? Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> Producttours { get; set; }
    }
}
