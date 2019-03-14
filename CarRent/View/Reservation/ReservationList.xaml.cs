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
    /// Interaction logic for ReservationList.xaml
    /// </summary>
    public partial class ReservationList : UserControl
    {
        carRentEntities _db = new carRentEntities();
        public static DataGrid dataGrid;

        public ReservationList()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            ReservationGrid.ItemsSource = _db.Reservations.ToList();
            dataGrid = ReservationGrid;
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int Id = (ReservationGrid.SelectedItem as Reservation).ReservationId;
            var deleteReservation = _db.Reservations.Where(c => c.ReservationId == Id).SingleOrDefault();
            var deleteRental = _db.RentalAgreements.Where(c => c.ReservationId == deleteReservation.ReservationId).SingleOrDefault();
            _db.RentalAgreements.Remove(deleteRental);
            _db.Reservations.Remove(deleteReservation);
            _db.SaveChanges();
            ReservationGrid.ItemsSource = _db.Reservations.ToList();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int Id = (ReservationGrid.SelectedItem as Reservation).ReservationId;
            UpdateReservation updateReservation = new UpdateReservation(Id);
            updateReservation.ShowDialog();
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            Reserve reserve = new Reserve();
            reserve.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           var source= _db.Reservations.Where(x => x.Source == tbsearchSource.Text).ToList();
            ReservationGrid.ItemsSource = source;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReservationGrid.ItemsSource = _db.Reservations.ToList();
        }
    }
}
