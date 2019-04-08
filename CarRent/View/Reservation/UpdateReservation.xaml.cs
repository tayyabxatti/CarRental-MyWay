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
        public List<Car> CarList { get; set; }
        public List<Client> ClientList { get; set; }
        public List<Staff> StaffList { get; set; }
        public List<Driver> DriverList { set; get; }
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
            var reser = _db.Reservations.Where(x => x.ReservationId == Id).SingleOrDefault();
            var cars = _db.Cars.ToList();
            var clients = _db.Clients.ToList();
            var staff = _db.Staffs.ToList();
            var driver = _db.Drivers.ToList();
            

            
            CarList = cars;
            cbCarMake.DataContext = CarList;
            
            
            ClientList = clients;
            cbRentersName.DataContext = ClientList;
           
            
            StaffList = staff;
            tbStaffName.DataContext = StaffList;

            
            DriverList = driver;
            cbDriverName.DataContext = DriverList;
            

            //var carnames = _db.Cars.ToList();
            //foreach (var item in carnames)
            //{
            //    cbCarMake.Items.Add($"{item.CarMake} : {item.CarRegistrationNo}");
            //}
            //var clientnames = _db.Clients.ToList();
            //foreach (var client in clientnames)
            //{
            //    cbRentersName.Items.Add($"{client.ClientName}");
            //}
            //var drivernames = _db.Drivers.ToList();
            //foreach (var item in drivernames)
            //{
            //    cbDriverName.Items.Add($"{item.DriverName}");
            //}

            tbBookedAtDATE.Minimum = DateTime.Now;
            tbBookedAtDATE.ClipValueToMinMax = true;
        }
        private void CbDriverName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            var item = cbRentersName.SelectedItem as Client;
            tbPickupAddress.Text = item.ClientPickUpAddress;
            tbTelephoneContact.Text = item.ClientContactNo;
            tbBillingAddress.Text = item.ClientCompanyName;
        }
        public void Load()
        {
            Reservation updateReservation = (from r in _db.Reservations where r.ReservationId == Id select r).SingleOrDefault();
            var carmake = _db.Cars.Where(c => c.CarId == updateReservation.CarId).SingleOrDefault();
            var drivername = _db.Drivers.Where(c => c.DriverId == updateReservation.DriverId).SingleOrDefault();
            var staffname = _db.Staffs.Where(c => c.StaffId == updateReservation.StaffId).SingleOrDefault();
            var rentername = _db.Clients.Where(c => c.ClientId == updateReservation.ClientId).SingleOrDefault();

            tbRentingStation.Text = updateReservation.RentingStation;
            tbBillingAddress.Text = updateReservation.BillingAddress;
            tbBookedAtDATE.Text = updateReservation.BookedAt.ToString();
            tbTelephoneContact.Text = updateReservation.Client.ClientContactNo;
            tbPickupAddress.Text = updateReservation.Client.ClientPickUpAddress;
            tbSource.Text = updateReservation.Source;
            tbRentingStation.Text = updateReservation.RentingStation;
            tbNote.Text = updateReservation.Note;
            tbCheckInStation.Text = updateReservation.CheckInStation;
            cbCarMake.SelectedItem = carmake;
            cbDriverName.SelectedItem = drivername;
            cbRentersName.SelectedItem = rentername;
            tbStaffName.SelectedItem = staffname;
            

            if (updateReservation.MethodOfPayment == "cash")
            {
                cbMethodOfPaymentCash.IsChecked = true;
                cbMethodOfPaymentCredit.IsChecked = false;
            }
            else
            {
                cbMethodOfPaymentCredit.IsChecked = true;
                cbMethodOfPaymentCash.IsChecked = false;
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
            var staffname = tbStaffName.SelectedItem as Staff;
            updateReservation.StaffId = staffname.StaffId;
            updateReservation.RentingStation = tbRentingStation.Text;
            updateReservation.Note = tbNote.Text;
            _db.SaveChanges();
            ReservationList.dataGrid.ItemsSource = _db.Reservations.ToList();
            this.Hide();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            tbBillingAddress.Text = "";
            tbBookedAtDATE.Text = "";
            cbRentersName.Text = "";
            tbTelephoneContact.Text = "";
            tbPickupAddress.Text = "";
            tbSource.Text ="";
            tbStaffName.Text = "";
            tbRentingStation.Text = "";
           
            tbNote.Text = "";
            cbDriverName.SelectedItem = "";
            cbRentersName.SelectedItem = "";
            tbCheckInStation.Text ="";
            cbCarMake.SelectedValue = "";

                cbMethodOfPaymentCash.IsChecked = false;
                cbMethodOfPaymentCredit.IsChecked = false;
        }
    }
}
