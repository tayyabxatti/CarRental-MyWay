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
        public Reserve()
        {
            InitializeComponent();
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
                cbRentersName.Items.Add($"{client.ClientName} : {client.ClientContactNo}");
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
        private void BtnInsert_Click(object sender, RoutedEventArgs e)
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
                ClientContactNo = cbRentersName.SelectedValue.ToString().Split(':')[1].Trim(),
                ClientName = cbRentersName.SelectedValue.ToString().Split(':')[0].Trim(),
            };
            _db.SaveChanges();
            Car car = new Car()
            {
                CarMake = cbCarMake.SelectedValue.ToString().Split(':')[0].Trim(),
                CarRegistrationNo = tbCarRegistrationNo.Text,
            };
            var cid = _db.Clients.Where(c => c.ClientName == client.ClientName && c.ClientContactNo == client.ClientContactNo).Select(a => a.ClientId).SingleOrDefault();
            var carid = _db.Cars.Where(c => c.CarMake == car.CarMake && c.CarRegistrationNo == tbCarRegistrationNo.Text).Select(f => f.CarId).SingleOrDefault();
            var did = _db.Drivers.Where(x => x.DriverName == cbDriverName.Text).Select(f => f.DriverId).SingleOrDefault();
            _db.SaveChanges();
            Reservation reservation = new Reservation()
            {
                RentingStation = tbRentingStation.Text,
                BookedAt = DateTime.Parse(tbBookedAtDATE.Text),
                //Client Airplane and Flight No
                CarId = carid,
                //Car Group Make and Model 
                ClientId = cid,
                //Driver Name
                DriverId = did,
                CheckInStation = tbCheckInStation.Text,
                //Client Name , Pickup Address
                MethodOfPayment = meth,
                BillingAddress = tbBillingAddress.Text,
                Source = tbSource.Text,
                //Client Telephone contact
                StaffName = tbStaffName.Text,
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
        

    }
}
