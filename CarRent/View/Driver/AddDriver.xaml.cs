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

        public AddDriver()
        {
            InitializeComponent();
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            Driver driver = new Driver()
            {
                DriverName = tbDriverName.Text,
            };
            _db.Drivers.Add(driver);
            _db.SaveChanges();
            DriverMenu.dataGrid.ItemsSource = _db.Drivers.ToList();
            this.Hide();
        }
    }
}
