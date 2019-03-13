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
    public partial class Vehicle : UserControl
    {
        carRentEntities _db = new carRentEntities();
        public static DataGrid dataGrid;
        public Vehicle()
        {
            InitializeComponent();
            Load();
            
        }
        private void Load()
        {
            VehicleGrid.ItemsSource = _db.Cars.ToList();
            dataGrid = VehicleGrid;
        }
        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            AddCar addCar = new AddCar();
            addCar.ShowDialog();
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int Id = (VehicleGrid.SelectedItem as Car).CarId;
            UpdateCar updateCar = new UpdateCar(Id);
            updateCar.ShowDialog();
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int Id = (VehicleGrid.SelectedItem as Car).CarId;
            var deleteCar = _db.Cars.Where(c => c.CarId == Id).SingleOrDefault();
            var deleteReservation = _db.Reservations.Where(c => c.CarId == Id).SingleOrDefault();
            _db.Cars.Remove(deleteCar);
            _db.Reservations.Remove(deleteReservation);
            _db.SaveChanges();
            VehicleGrid.ItemsSource = _db.Cars.ToList();
        }
    }
}
