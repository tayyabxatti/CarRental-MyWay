using CarRent.View.Renters;
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
    /// Interaction logic for Reserve.xaml
    /// </summary>
    public partial class Reserve : Window
    {
        carRentEntities _db = new carRentEntities();
        int Id;
        public List<Car> CarList { get; set; }
        public List<Client> ClientList { get; set; }
        public List<Staff> StaffList { get; set; }
        public List<Driver> DriverList { set; get; }
        public Reserve(int ReservationId)
        {
            InitializeComponent();
            Id = ReservationId;
            LoadorInsert();
            fillCombo();
        }

        public void LoadorInsert()
        {
            if (Id != 0)
            {
                Reservation updateReservation = (from r in _db.Reservations where r.ReservationId == Id select r).SingleOrDefault();
                var carmake = _db.Cars.Where(c => c.CarId == updateReservation.CarId).SingleOrDefault();
                var drivername = _db.Drivers.Where(c => c.DriverId == updateReservation.DriverId).SingleOrDefault();
                var staffname = _db.Staffs.Where(c => c.StaffId == updateReservation.StaffId).SingleOrDefault();
                var rentername = _db.Clients.Where(c => c.ClientId == updateReservation.ClientId).SingleOrDefault();

                
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
        }

        public void fillCombo()
        {
            var cars = _db.Cars.ToList();
            CarList = cars;
            cbCarMake.DataContext = CarList;

            var clients = _db.Clients.ToList();
            ClientList = clients;
            cbRentersName.DataContext = ClientList;

            var staff = _db.Staffs.ToList();
            StaffList = staff;
            tbStaffName.DataContext = StaffList;

            var driver = _db.Drivers.ToList();
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

            //        cbRentersName.Items.Add($"{client.ClientName} : {client.ClientContactNo}");

            //}
            //var drivernames = _db.Drivers.ToList();
            //foreach (var item in drivernames)
            //{
            //    cbDriverName.Items.Add($"{item.DriverName}");
            //}
            //var staffnames = _db.Staffs.ToList();
            //foreach(var lad in staffnames)
            //{
            //    tbStaffName.Items.Add(lad.StaffName);
            //}

            tbBookedAtDATE.Minimum = DateTime.Now;
            tbBookedAtDATE.ClipValueToMinMax = true;
        }
        

        private void CbSelfDrive_Checked(object sender, RoutedEventArgs e)
        {
            if (cbSelfDrive.IsChecked == true)
            {
                cbDriver.IsChecked = false;
                cbDriver.Visibility = Visibility.Hidden;
                cbDriverName.Visibility = Visibility.Hidden;
            }
            
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
        
        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (Id == 0)
            {
                string meth = "";
                if (cbMethodOfPaymentCash.IsChecked == true)
                {
                    meth = "Cash";
                }
                else { meth = "Credit"; }

                Client client = new Client()
                {

                    ClientPickUpAddress = tbPickupAddress.Text,
                    ClientContactNo = tbTelephoneContact.Text,
                    ClientName = cbRentersName.SelectedValue.ToString(),
                };
                _db.SaveChanges();
                Car car = new Car()
                {
                    CarId = Convert.ToInt32(cbCarMake.SelectedValue),
                    CarMake = cbCarMake.SelectedValue.ToString(),

                };
                Staff staff = new Staff()
                {
                    StaffId = Convert.ToInt32(tbStaffName.SelectedValue),
                    StaffName = tbStaffName.SelectedItem.ToString(),
                };

                var cid = _db.Clients.Where(c => c.ClientName == client.ClientName && c.ClientContactNo == client.ClientContactNo).Select(a => a.ClientId).SingleOrDefault();
                var carid = _db.Cars.Where(c => c.CarId == car.CarId).Select(f => f.CarId).SingleOrDefault();
                var did = _db.Drivers.Where(x => x.DriverName == cbDriverName.Text).Select(f => f.DriverId).SingleOrDefault();
                _db.SaveChanges();
                Reservation reservation = new Reservation()
                {
                    RentingStation = tbRentingStation.Text,
                    BookedAt = DateTime.Parse(tbBookedAtDATE.Text),
                    //Client Airplane and Flight No
                    CarId = Convert.ToInt32(cbCarMake.SelectedValue),
                    //Car Group Make and Model 
                    ClientId = Convert.ToInt32(cbRentersName.SelectedValue),
                    //Driver Name
                    DriverId = Convert.ToInt32(cbDriverName.SelectedValue),
                    CheckInStation = tbCheckInStation.Text,
                    //Client Name , Pickup Address
                    MethodOfPayment = meth,
                    BillingAddress = tbBillingAddress.Text,
                    Source = tbSource.Text,
                    //Client Telephone contact
                    StaffId = Convert.ToInt32(tbStaffName.SelectedValue),
                    Note = tbNote.Text,
                    ReservationDateTime = DateTime.Now,

                };
                _db.Reservations.Add(reservation);
                _db.SaveChanges();
                RentalAgreement rentalAgreement = new RentalAgreement()
                {
                    ReservationId = reservation.ReservationId,
                };
                _db.RentalAgreements.Add(rentalAgreement);
                _db.SaveChanges();
                ReservationList.dataGrid.ItemsSource = _db.Reservations.ToList();
                this.Hide();
            }
            else
            {
                Reservation updateReservation = (from r in _db.Reservations where r.ReservationId == Id select r).SingleOrDefault();
                var client = cbRentersName.SelectedItem as Client;
                var car = cbCarMake.SelectedItem as Car;
                var staff = tbStaffName.SelectedItem as Staff;
                var driver = cbDriverName.SelectedItem as Driver;
                var x = driver.DriverId;

                //var clientId = _db.Clients.Where(c => c.ClientId == updateReservation.ClientId).Select(x => x.ClientId).SingleOrDefault();
                //var driverId = _db.Drivers.Where(c => c.DriverId == updateReservation.DriverId).Select(x => x.DriverId).SingleOrDefault();
                //var CarId = _db.Cars.Where(c => c.CarId == updateReservation.CarId).Select(x=>x.CarId).SingleOrDefault();
                string meth;
                if (cbMethodOfPaymentCash.IsChecked == true)
                {
                    meth = "Cash";
                }
                else { meth = "Credit"; }
                updateReservation.DriverId = driver.DriverId;
                _db.SaveChanges();
                updateReservation.CarId = car.CarId;
                _db.SaveChanges();
                updateReservation.ClientId = client.ClientId;
                _db.SaveChanges();
                updateReservation.BillingAddress = tbBillingAddress.Text;
                updateReservation.BookedAt = DateTime.Parse(tbBookedAtDATE.Text);
                updateReservation.MethodOfPayment = meth;
                updateReservation.Source = tbSource.Text;
                updateReservation.ReservationDateTime = DateTime.Now;
                updateReservation.StaffId = staff.StaffId;
                updateReservation.RentingStation = tbRentingStation.Text;
                updateReservation.Note = tbNote.Text;
                _db.SaveChanges();
                ReservationList.dataGrid.ItemsSource = _db.Reservations.ToList();
                this.Hide();
            }
        }

        private void BtnAddRenter_Click(object sender, RoutedEventArgs e)
        {
            AddRenter addRenter = new AddRenter(0);
            addRenter.ShowDialog();
            fillCombo();
        }


        private void BtnAddCar_Click(object sender, RoutedEventArgs e)
        {
            AddCar addCar = new AddCar(0);
            addCar.ShowDialog();
            fillCombo();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            tbBillingAddress.Text =null;
            tbBookedAtDATE.Text = null;
            cbRentersName.Text = null;
            tbTelephoneContact.Text = null;
            tbPickupAddress.Text = null;
            tbSource.Text = null;
            tbStaffName.Text = null;
            tbRentingStation.Text = null;
            tbNote.Text = null;
            cbDriverName.SelectedItem = null;
            cbRentersName.SelectedItem = null;
            tbCheckInStation.Text = null;
            cbCarMake.SelectedValue = null;

           cbMethodOfPaymentCash.IsChecked = false;
           cbMethodOfPaymentCredit.IsChecked = false;
            
        }

        private void CbRentersName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cbRentersName.SelectedItem as Client;
            tbPickupAddress.Text = item.ClientPickUpAddress;
            tbTelephoneContact.Text = item.ClientContactNo;
            tbBillingAddress.Text = item.ClientCompanyName;
            //if (cbRentersName.SelectedValue != null)
            //{
            //    var clientName = cbRentersName.SelectedValue.ToString()?.Split(':')[0].Trim();
            //    tbPickupAddress.Text = _db.Clients.Where(a => a.ClientName == clientName).Select(x => x.ClientPickUpAddress).SingleOrDefault();
            //    tbTelephoneContact.Text = _db.Clients.Where(a => a.ClientName == clientName).Select(x => x.ClientContactNo).SingleOrDefault();
            //    tbBillingAddress.Text = _db.Clients.Where(x => x.ClientName == clientName).Select(a => a.ClientCompanyName).SingleOrDefault();
            //}
        }

        private void CbDriverName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void TbStaffName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }

        private void CbSelfDrive_Unchecked(object sender, RoutedEventArgs e)
        {
            if (cbSelfDrive.IsChecked == false)
            {
                cbDriver.IsChecked = false;
                cbDriver.Visibility = Visibility.Visible;
                cbDriverName.Visibility = Visibility.Visible;
            }

        }
    }


}

