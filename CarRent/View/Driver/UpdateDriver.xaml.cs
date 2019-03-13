using System;
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

namespace CarRent.View
{
    /// <summary>
    /// Interaction logic for UpdateDriver.xaml
    /// </summary>
    public partial class UpdateDriver : Window
    {
        carRentEntities _db = new carRentEntities();
        int Id;
        public UpdateDriver(int DriverId)
        {
            InitializeComponent();
            Id = DriverId;
            Load();

        }
        public void Load()
        {

            var updateDriver = _db.Drivers.Where(d => d.DriverId == Id).SingleOrDefault();
            tbDriverName.Text = updateDriver.DriverName;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var updateDriver = _db.Drivers.Where(d => d.DriverId == Id).SingleOrDefault();
            updateDriver.DriverName= tbDriverName.Text;
            _db.SaveChanges();
            DriverMenu.dataGrid.ItemsSource = _db.Drivers.ToList();
            this.Hide();
        }
    }
}
