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
    /// Interaction logic for UpdateCar.xaml
    /// </summary>
    public partial class UpdateCar : Window
    {
        carRentEntities _db = new carRentEntities();
        int Id;
        public UpdateCar(int CarId)
        {
            InitializeComponent();
            Id = CarId;
            Load();
        }
        public void Load()
        {
            var updateCar = (from c in _db.Cars where c.CarId == Id select c).SingleOrDefault();
               tbCarKmIn.Text = updateCar.CarKmIn.ToString(); 
               tbCarKmOut.Text =updateCar.CarKmOut.ToString();
               tbCarMake.Text = updateCar.CarMake;    
               tbCarRegistrationNo.Text=updateCar.CarRegistrationNo;
                tbDateIn.SelectedDate = DateTime.Parse(updateCar.DateIn);
            tbDateOut.SelectedDate = DateTime.Parse(updateCar.DateOut);
            tbKmBill.Text = updateCar.KmBill.ToString();
               tbTImeIn.Value = DateTime.Parse(updateCar.TImeIn);    
               tbTimeOut.Value = DateTime.Parse(updateCar.TimeOut);   
               tbTimeBill.Text = updateCar.TimeBill.ToString();
               
            if(updateCar.CarOwner == "Own")
            {
                cbCarOwnerOwn.IsChecked = true;
            }
            else if(updateCar.CarOwner == "Investor")
            {
                cbCarOwnerInvestor.IsChecked = true;
            }
            else
            {
                cbCarOwnerNonPool.IsChecked = true;
            }

            if(updateCar.CarFuelState =="Full")
            {
                cbCarFuelStateFull.IsChecked = true;
            }
            else if(updateCar.CarFuelState == "Quarter")
            {
                cbCarFuelStateQuarter.IsChecked = true;
            }
            else if(updateCar.CarFuelState == "Half")
            {
                cbCarFuelStateHalf.IsChecked = true;
            }
            else
            {
                cbCarFuelStateEmpty.IsChecked = true;
                    
            }

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Car updateCar = (from c in _db.Cars where c.CarId == Id select c).SingleOrDefault();
            var totalKm = Int32.Parse(tbCarKmIn.Text) - Int32.Parse(tbCarKmOut.Text);
            var totalTime = DateTime.Parse(tbTImeIn.Value.Value.ToLongTimeString()) - DateTime.Parse(tbTimeOut.Value.Value.ToLongTimeString());

            updateCar.CarKmIn = Int32.Parse(tbCarKmIn.Text);
            updateCar.CarKmOut =Int32.Parse(tbCarKmOut.Text);
            updateCar.CarMake = tbCarMake.Text;
            updateCar.CarRegistrationNo = tbCarRegistrationNo.Text;
            updateCar.DateIn = tbDateIn.SelectedDate.Value.ToShortDateString();
            updateCar.DateOut =tbDateOut.SelectedDate.Value.ToShortDateString();
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

            if(cbCarFuelStateFull.IsChecked == true)
            {
                updateCar.CarFuelState = cbCarFuelStateFull.Content.ToString();
            }
            else if (cbCarFuelStateQuarter.IsChecked == true)
            {
                updateCar.CarFuelState = cbCarFuelStateQuarter.Content.ToString();
            }
            else if(cbCarFuelStateHalf.IsChecked == true)
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
            if (cbCarFuelStateQuarter.IsChecked == true)
            {
                cbCarFuelStateFull.IsChecked = false;
                cbCarFuelStateEmpty.IsChecked = false;
                cbCarFuelStateHalf.IsChecked = false;

            }

        }

        private void CbCarFuelStateHalf_Checked(object sender, RoutedEventArgs e)
        {
            if (cbCarFuelStateHalf.IsChecked == true)
            {
                cbCarFuelStateFull.IsChecked = false;
                cbCarFuelStateEmpty.IsChecked = false;
                cbCarFuelStateQuarter.IsChecked = false;
            }


        }

        private void CbCarFuelStateEmpty_Checked(object sender, RoutedEventArgs e)
        {
            if (cbCarFuelStateEmpty.IsChecked == true)
            {
                cbCarFuelStateFull.IsChecked = false;
                cbCarFuelStateQuarter.IsChecked = false;
                cbCarFuelStateHalf.IsChecked = false;
            }
        }
    }
}
