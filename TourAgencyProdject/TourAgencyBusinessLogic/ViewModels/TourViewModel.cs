using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TourAgencyBusinessLogic.ViewModels
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class TourViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        public string tourName { get; set; }
    }
}
