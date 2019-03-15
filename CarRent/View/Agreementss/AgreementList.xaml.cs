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

namespace CarRent.View.Agreementss
{
    /// <summary>
    /// Interaction logic for AgreementList.xaml
    /// </summary>
    public partial class AgreementList : UserControl
    {
        carRentEntities _db = new carRentEntities();
        public static DataGrid dataGrid;
        public AgreementList()
        {
            InitializeComponent();
            Load();
        }
        public void Load()
        {
            var combobox = _db.RentalAgreements.ToList();
            foreach(var item in combobox)
            {
                cbsearchReservationId.Items.Add(item.ReservationId.ToString());
            }
            AgreemenGrid.ItemsSource = _db.RentalAgreements.ToList();
            dataGrid = AgreemenGrid;
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int Id = (AgreemenGrid.SelectedItem as RentalAgreement).RentalAgreementId;
            AgreementUpdate agreementUpdate = new AgreementUpdate(Id);
            agreementUpdate.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(cbsearchClosed.IsChecked==true && cbsearchReservationId.Text != "")
            {
                var rentalSearch = _db.RentalAgreements.Where(x => x.AgreementClosed == "Closed" && x.ReservationId == Convert.ToInt32(cbsearchReservationId.SelectedValue)).ToList();
                AgreemenGrid.ItemsSource = rentalSearch;
            }
            else if(cbsearchClosed.IsChecked==false && cbsearchReservationId.Text != "")
            {
                int soso = Int32.Parse(cbsearchReservationId.SelectedItem.ToString());
                var rentalsearch = _db.RentalAgreements.Where(x => x.ReservationId ==soso ).ToList();
                AgreemenGrid.ItemsSource = rentalsearch;
            }
            else if(cbsearchClosed.IsChecked == true)
            {
                var closed = _db.RentalAgreements.Where(x => x.AgreementClosed == "Closed").ToList();
                AgreemenGrid.ItemsSource = closed;
            }

        }

        private void CbsearchReservationId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var list = _db.RentalAgreements.ToList();
            AgreemenGrid.ItemsSource = list;
        }
        //private void BtnView_Click(object sender, RoutedEventArgs e)
        //{
        //    int Id = (AgreemenGrid.SelectedItem as RentalAgreement).RentalAgreementId;
        //    AgreementView agreementView = new AgreementView(Id);
        //    agreementView.ShowDialog();
        //}

    }
}
