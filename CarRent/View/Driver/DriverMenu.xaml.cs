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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRent.View
{
    /// <summary>
    /// Interaction logic for DriverMenu.xaml
    /// </summary>
    public partial class DriverMenu : UserControl
    {
        carRentEntities _db = new carRentEntities();
        public static DataGrid dataGrid;

        public DriverMenu()
        {
            InitializeComponent();
            Load();
        }
        private void Load()
        {
            DriverGrid.ItemsSource = _db.Drivers.ToList();
            dataGrid = DriverGrid;
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int Id = (DriverGrid.SelectedItem as Driver).DriverId;
            AddDriver updateDriver = new AddDriver(Id);
            updateDriver.ShowDialog();

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int Id = (DriverGrid.SelectedItem as Driver).DriverId;
            var deleteDriver = _db.Drivers.Where(c => c.DriverId == Id).SingleOrDefault();
            _db.Drivers.Remove(deleteDriver);
            _db.SaveChanges();
            DriverGrid.ItemsSource = _db.Drivers.ToList();
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            
            AddDriver driver = new AddDriver(0);
            driver.ShowDialog();

        }
    }
}
