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
    /// Interaction logic for AddDriver.xaml
    /// </summary>
    public partial class AddDriver : Window
    {
        carRentEntities _db = new carRentEntities();
        int Id;

        public AddDriver(int DriverId)
        {
            InitializeComponent();
            Id = DriverId;
            LoadorInsert();
        }
        public void LoadorInsert()
        {
            if (Id != 0)
            {
                UpdateLoad();
            }
            
        }

        public void UpdateLoad()
        {
            var updateDriver = _db.Drivers.Where(d => d.DriverId == Id).SingleOrDefault();
            tbDriverName.Text = updateDriver.DriverName;
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (Id == 0) { 

            Driver driver = new Driver()
            {
                DriverName = tbDriverName.Text,
            };
            _db.Drivers.Add(driver);
            _db.SaveChanges();
            DriverMenu.dataGrid.ItemsSource = _db.Drivers.ToList();
            this.Hide();
            }
            else
            {
                var updateDriver = _db.Drivers.Where(d => d.DriverId == Id).SingleOrDefault();
                updateDriver.DriverName = tbDriverName.Text;
                _db.SaveChanges();
                DriverMenu.dataGrid.ItemsSource = _db.Drivers.ToList();
                this.Hide();

            }
        }
            
    }
}
