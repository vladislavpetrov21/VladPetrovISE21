using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TourAgencyBusinessLogic.Interfaces;
using TourAgencyBusinessLogic.ViewModels;
using Unity;

namespace TourAgencyView
{
    public partial class FormVoucherTour : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id
        {
            get { return Convert.ToInt32(comboBoxVoucherTour.SelectedValue); }
            set { comboBoxVoucherTour.SelectedValue = value; }
        }
        public string tourName { get { return comboBoxVoucherTour.Text; } }
        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }
        public FormVoucherTour(ITourLogic logic)
        {
            InitializeComponent();
            List<TourViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxVoucherTour.DisplayMember = "tourName";
                comboBoxVoucherTour.ValueMember = "Id";
                comboBoxVoucherTour.DataSource = list;
                comboBoxVoucherTour.SelectedItem = null;
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxVoucherTour.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
