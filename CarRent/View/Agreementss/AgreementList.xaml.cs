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
            AgreemenGrid.ItemsSource = _db.RentalAgreements.ToList();
            dataGrid = AgreemenGrid;
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int Id = (AgreemenGrid.SelectedItem as RentalAgreement).RentalAgreementId;
            AgreementUpdate agreementUpdate = new AgreementUpdate(Id);
            agreementUpdate.ShowDialog();
        }
        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            int Id = (AgreemenGrid.SelectedItem as RentalAgreement).RentalAgreementId;
            AgreementView agreementView = new AgreementView(Id);
            agreementView.ShowDialog();
        }

    }
}
