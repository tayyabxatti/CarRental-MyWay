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
    /// Interaction logic for UpdateReservation.xaml
    /// </summary>
    public partial class UpdateReservation : Window
    {
        carRentEntities _db = new carRentEntities();
        int Id;
        public UpdateReservation(int ReservationId)
        {
            InitializeComponent();
            Id = ReservationId;
            Load();
            fillCombo();
        }
        public void fillCombo()
        {

            var carnames = _db.Cars.ToList();
            foreach (var item in carnames)
            {
                cbCarMake.Items.Add($"{item.CarMake} : {item.CarRegistrationNo}");
            }
            var clientnames = _db.Clients.ToList();
            foreach (var client in clientnames)
            {
                cbRentersName.Items.Add($"{client.ClientName}");
            }
            var drivernames = _db.Drivers.ToList();
            foreach (var item in drivernames)
            {
                cbDriverName.Items.Add($"{item.DriverName}");
            }
        }
        private void CbDriverName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void CbCarMake_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbCarRegistrationNo.Text = cbCarMake.SelectedValue.ToString().Split(':')[1].Trim();
        }
        private void CbSelfDrive_Checked(object sender, RoutedEventArgs e)
        {
            if (cbSelfDrive.IsChecked == true)
            {
                cbDriver.IsChecked = false;
            }
            cbDriverName.Text = null;
        }
        private void CbDriver_Checked(object sender, RoutedEventArgs e)
        {
            if (cbDriver.IsChecked == true)
            {
                cbSelfDrive.IsChecked = false;
            }
            cbDriverName.Text = "Driver Name:";

        }
        private void CbMethodOfPaymentCredit_Checked(object sender, RoutedEventArgs e)
        {
            if (cbMethodOfPaymentCredit.IsChecked == true)
            {
                cbMethodOfPaymentCash.IsChecked = false;
            }
        }
        private void CbMethodOfPaymentCash_Checked(object sender, RoutedEventArgs e)
        {
            if (cbMethodOfPaymentCash.IsChecked == true)
            {
                cbMethodOfPaymentCredit.IsChecked = false;
            }
        }
        private void CbRentersName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var clientName = cbRentersName.SelectedValue.ToString();
            tbPickupAddress.Text = _db.Clients.Where(a => a.ClientName == clientName).Select(x => x.ClientPickUpAddress).SingleOrDefault();
            tbTelephoneContact.Text = _db.Clients.Where(a => a.ClientName == clientName).Select(x => x.ClientContactNo).SingleOrDefault();
        }
        public void Load()
        {
            Reservation updateReservation = (from r in _db.Reservations where r.ReservationId == Id select r).SingleOrDefault();

            tbBillingAddress.Text = updateReservation.BillingAddress;
            tbBookedAtDATE.Text = updateReservation.BookedAt.ToString();
            cbRentersName.Text = updateReservation.Client.ClientName;
            tbTelephoneContact.Text = updateReservation.Client.ClientContactNo;
            tbPickupAddress.Text = updateReservation.Client.ClientPickUpAddress;
            tbSource.Text = updateReservation.Source;
            tbStaffName.Text = updateReservation.StaffName;
            tbRentingStation.Text = updateReservation.RentingStation;
            tbCarRegistrationNo.Text = updateReservation.Car.CarRegistrationNo;
            tbNote.Text = updateReservation.Note;
            cbDriverName.SelectedItem = updateReservation.Driver.DriverName;
            cbRentersName.SelectedItem = updateReservation.Client.ClientName;
            tbCheckInStation.Text = updateReservation.CheckInStation;
            cbCarMake.SelectedValue = updateReservation.Car.CarMake;

            if (updateReservation.MethodOfPayment == "cash")
            {
                cbMethodOfPaymentCash.IsChecked = true;
            }
            else
            {
                cbMethodOfPaymentCredit.IsChecked = true;
            }
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Reservation updateReservation = (from r in _db.Reservations where r.ReservationId == Id select r).SingleOrDefault();
            var clientId = (from c in _db.Clients where c.ClientName == cbRentersName.Text select c.ClientId).SingleOrDefault();
            var driverId = (from d in _db.Drivers where d.DriverName == cbDriverName.Text select d.DriverId).SingleOrDefault();
            //var CarId = (from c in _db.Cars where c.CarMake == cbCarMake.SelectedItem.ToString().Split(':')[0].Trim() select c.CarId).SingleOrDefault();
            string meth;
            if (cbMethodOfPaymentCash.IsChecked == true)
            {
                meth = "Cash";
            }
            else { meth = "Credit"; }
            updateReservation.DriverId = driverId;
            _db.SaveChanges();
            //updateReservation.CarId = CarId;
            _db.SaveChanges();
            updateReservation.ClientId = clientId;
            _db.SaveChanges();
            updateReservation.BillingAddress = tbBillingAddress.Text;
            updateReservation.BookedAt = DateTime.Parse(tbBookedAtDATE.Text);
            updateReservation.MethodOfPayment = meth;
            updateReservation.Source = tbSource.Text;
            updateReservation.ReservationDateTime = DateTime.Now;
            updateReservation.StaffName = tbStaffName.Text;
            updateReservation.RentingStation = tbRentingStation.Text;
            updateReservation.Note = tbNote.Text;
            _db.SaveChanges();
            ReservationList.dataGrid.ItemsSource = _db.Reservations.ToList();
            this.Hide();
        }
    }
}
