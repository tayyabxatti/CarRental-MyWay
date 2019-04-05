using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace CarRent.View
{
    /// <summary>
    /// Interaction logic for AddCar.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        carRentEntities _db = new carRentEntities();
        int Id;
        public AddCar(int CarId)
        {
            InitializeComponent();
            Id = CarId;
            LoadorInsert();
        }

        public void LoadorInsert()
        {
            if (Id != 0)
            {
                var updateCar = (from c in _db.Cars where c.CarId == Id select c).SingleOrDefault();
                tbCarKmIn.Text = updateCar.CarKmIn.ToString();
                tbCarKmOut.Text = updateCar.CarKmOut.ToString();
                tbCarMake.Text = updateCar.CarMake;
                tbCarRegistrationNo.Text = updateCar.CarRegistrationNo;
                tbDateIn.SelectedDate = DateTime.Parse(updateCar.DateIn);
                tbDateOut.SelectedDate = DateTime.Parse(updateCar.DateOut);
                tbKmBill.Text = updateCar.KmBill.ToString();
                tbTImeIn.Value = DateTime.Parse(updateCar.TImeIn);
                tbTimeOut.Value = DateTime.Parse(updateCar.TimeOut);
                tbTimeBill.Text = updateCar.TimeBill.ToString();
                tbCarKmIn.IsReadOnly = true;
                tbCarKmOut.IsReadOnly = true;
                if (updateCar.CarOwner == "Own")
                {
                    cbCarOwnerOwn.IsChecked = true;
                }
                else if (updateCar.CarOwner == "Investor")
                {
                    cbCarOwnerInvestor.IsChecked = true;
                }
                else
                {
                    cbCarOwnerNonPool.IsChecked = true;
                }

                if (updateCar.CarFuelState == "Full")
                {
                    cbCarFuelStateFull.IsChecked = true;
                }
                else if (updateCar.CarFuelState == "Quarter")
                {
                    cbCarFuelStateQuarter.IsChecked = true;
                }
                else if (updateCar.CarFuelState == "Half")
                {
                    cbCarFuelStateHalf.IsChecked = true;
                }
                else
                {
                    cbCarFuelStateEmpty.IsChecked = true;

                }
            }
            else
            {
                tbCarKmIn.Visibility = Visibility.Hidden;
                tbCarKmOut.Visibility = Visibility.Hidden;
                lbCarKmIn.Visibility = Visibility.Hidden;
                lbCarKmOut.Visibility = Visibility.Hidden;
            }
        }
        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            //var totalKm = 0;
            //if (tbCarKmIn.Text != "" && tbCarKmOut.Text != "" && Convert.ToInt32(tbCarKmIn.Text) > Convert.ToInt32(tbCarKmOut.Text))
            //{
            //    totalKm = Int32.Parse(tbCarKmIn.Text) - Int32.Parse(tbCarKmOut.Text);
            //}
            //else
            //{
            //    System.Windows.MessageBox.Show("please enter valid KmIn and KmOut");
            //}
            if (Id == 0)
            {
                var totalTime = DateTime.Now - DateTime.Now;
                if (tbTImeIn.Text != "" && tbTimeOut.Text != null && DateTime.Parse(tbTimeOut.Text) < DateTime.Parse(tbTImeIn.Text))
                {
                    totalTime = DateTime.Parse(tbTImeIn.Value.Value.ToLongTimeString()) - DateTime.Parse(tbTimeOut.Value.Value.ToLongTimeString());
                }
                else
                {
                    System.Windows.MessageBox.Show("Please enter valid TimeIn and TimeOut");
                }
                string carowner = "";
                if (cbCarOwnerOwn.IsChecked == true)
                {
                    carowner = cbCarOwnerOwn.Content.ToString();
                }
                else if (cbCarOwnerNonPool.IsChecked == true)
                {
                    carowner = cbCarOwnerNonPool.Content.ToString();
                }
                else
                {
                    carowner = cbCarOwnerInvestor.Content.ToString();
                }
                string fuelstate = "";
                if (cbCarFuelStateFull.IsChecked == true)
                {
                    fuelstate = cbCarFuelStateFull.Content.ToString();
                }
                else if (cbCarFuelStateHalf.IsChecked == true)
                {
                    fuelstate = cbCarFuelStateHalf.Content.ToString();
                }
                else if (cbCarFuelStateQuarter.IsChecked == true)
                {
                    fuelstate = cbCarFuelStateQuarter.Content.ToString();
                }
                else if (cbCarFuelStateEmpty.IsChecked == true)
                {
                    fuelstate = cbCarFuelStateEmpty.Content.ToString();
                }

                Car car = new Car()
                {
                    InitialMeterReading = Int32.Parse(tbInitalReading.Text),
                    CarKmIn = Int32.Parse(tbInitalReading.Text),
                    CarKmOut = Int32.Parse(tbInitalReading.Text),
                    CarMake = tbCarMake.Text,
                    CarRegistrationNo = tbCarRegistrationNo.Text,
                    DateIn = tbDateIn.SelectedDate.Value.ToShortDateString(),
                    DateOut = tbDateOut.SelectedDate.Value.ToShortDateString(),
                    KmBill = Int32.Parse(tbKmBill.Text),
                    TImeIn = tbTImeIn.Value.Value.ToShortTimeString(),
                    TimeOut = tbTimeOut.Value.Value.ToShortTimeString(),
                    TimeBill = Int32.Parse(tbTimeBill.Text),
                    //TotalKm = totalKm,
                    TotalTime = Convert.ToInt16(totalTime.TotalHours),
                    CarFuelState = fuelstate,
                    CarOwner = carowner,
                };
                if (car != null)
                {
                    _db.Cars.Add(car);
                    _db.SaveChanges();
                }
                else
                {
                    System.Windows.MessageBox.Show("Please Fill the Input Fields");
                }
                Vehicle.dataGrid.ItemsSource = _db.Cars.ToList();
                this.Hide();
            }
            else
            {
                Car updateCar = (from c in _db.Cars where c.CarId == Id select c).SingleOrDefault();
                var totalKm = Int32.Parse(tbCarKmIn.Text) - Int32.Parse(tbCarKmOut.Text);
                var totalTime = DateTime.Parse(tbTImeIn.Value.Value.ToLongTimeString()) - DateTime.Parse(tbTimeOut.Value.Value.ToLongTimeString());

                updateCar.CarKmIn = Int32.Parse(tbCarKmIn.Text);
                updateCar.CarKmOut = Int32.Parse(tbCarKmOut.Text);
                updateCar.CarMake = tbCarMake.Text;
                updateCar.CarRegistrationNo = tbCarRegistrationNo.Text;
                updateCar.DateIn = tbDateIn.SelectedDate.Value.ToShortDateString();
                updateCar.DateOut = tbDateOut.SelectedDate.Value.ToShortDateString();
                updateCar.KmBill = Int32.Parse(tbKmBill.Text);
                updateCar.TImeIn = tbTImeIn.Value.Value.ToShortTimeString();
                updateCar.TimeOut = tbTimeOut.Value.Value.ToShortTimeString();
                updateCar.TimeBill = Int32.Parse(tbTimeBill.Text);
                updateCar.TotalKm = totalKm;
                updateCar.TotalTime = Convert.ToInt16(totalTime.TotalHours.ToString());

                if (updateCar.CarOwner == "Own")
                {
                    updateCar.CarOwner = cbCarOwnerOwn.Content.ToString();
                }
                else if (updateCar.CarOwner == "Investor")
                {
                    updateCar.CarOwner = cbCarOwnerInvestor.Content.ToString();
                }
                else
                {
                    updateCar.CarOwner = cbCarOwnerNonPool.Content.ToString();
                }

                if (cbCarFuelStateFull.IsChecked == true)
                {
                    updateCar.CarFuelState = cbCarFuelStateFull.Content.ToString();
                }
                else if (cbCarFuelStateQuarter.IsChecked == true)
                {
                    updateCar.CarFuelState = cbCarFuelStateQuarter.Content.ToString();
                }
                else if (cbCarFuelStateHalf.IsChecked == true)
                {
                    updateCar.CarFuelState = cbCarFuelStateHalf.Content.ToString();
                }
                else
                {
                    updateCar.CarFuelState = cbCarFuelStateEmpty.Content.ToString();
                }
                _db.SaveChanges();
                Vehicle.dataGrid.ItemsSource = _db.Cars.ToList();
                this.Hide();
            }
        }

        private void CbCarOwnerOwn_Checked(object sender, RoutedEventArgs e)
        {
            if (cbCarOwnerOwn.IsChecked == true)
            {
                cbCarOwnerNonPool.IsChecked = false;
                cbCarOwnerInvestor.IsChecked = false; 
            }
        }

        private void CbCarOwnerInvestor_Checked(object sender, RoutedEventArgs e)
        {
            if (cbCarOwnerInvestor.IsChecked == true)
            {
                cbCarOwnerOwn.IsChecked = false;
                cbCarOwnerNonPool.IsChecked = false;
            }
        }

        private void CbCarOwnerNonPool_Checked(object sender, RoutedEventArgs e)
        {
            if (cbCarOwnerNonPool.IsChecked == true)
            {
                cbCarOwnerOwn.IsChecked = false;
                cbCarOwnerInvestor.IsChecked = false;
            }
        }

        private void CbCarFuelStateFull_Checked(object sender, RoutedEventArgs e)
        {
            if (cbCarFuelStateFull.IsChecked == true)
            {
                cbCarFuelStateHalf.IsChecked = false;
                cbCarFuelStateQuarter.IsChecked = false;
                cbCarFuelStateEmpty.IsChecked = false;
            }
        }

        private void CbCarFuelStateQuarter_Checked(object sender, RoutedEventArgs e)
        {
            if(cbCarFuelStateQuarter.IsChecked == true)
            {
                cbCarFuelStateFull.IsChecked = false;
                cbCarFuelStateEmpty.IsChecked = false;
                cbCarFuelStateHalf.IsChecked = false;

            }

        }

        private void CbCarFuelStateHalf_Checked(object sender, RoutedEventArgs e)
        {
            if(cbCarFuelStateHalf.IsChecked == true)
            {
                cbCarFuelStateFull.IsChecked = false;
                cbCarFuelStateEmpty.IsChecked = false;
                cbCarFuelStateQuarter.IsChecked = false;
            }


        }

        private void CbCarFuelStateEmpty_Checked(object sender, RoutedEventArgs e)
        {
            if(cbCarFuelStateEmpty.IsChecked == true)
            {
                cbCarFuelStateFull.IsChecked = false;
                cbCarFuelStateQuarter.IsChecked = false;
                cbCarFuelStateHalf.IsChecked = false;
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

       
    }
}
