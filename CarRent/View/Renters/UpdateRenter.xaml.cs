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

namespace CarRent.View.Renters
{
    /// <summary>
    /// Interaction logic for UpdateRenter.xaml
    /// </summary>
    public partial class UpdateRenter : Window
    {
        carRentEntities _db = new carRentEntities();
        int Id;
        public UpdateRenter(int clientId)
        {
            InitializeComponent();
            Id = clientId;
            Load();
        }

        public void Load()
        {
            var updateClient = _db.Clients.Where(x => x.ClientId == Id).SingleOrDefault();
            tbClientCompanyName.Text = updateClient.ClientCompanyName;
            tbClientContactNo.Text = updateClient.ClientContactNo;
            tbClientName.Text = updateClient.ClientName;
            tbClientPickUpAddress.Text = updateClient.ClientPickUpAddress;
            
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var updateClient = _db.Clients.Where(d => d.ClientId == Id).SingleOrDefault();
            updateClient.ClientName = tbClientName.Text;
            updateClient.ClientPickUpAddress =  tbClientPickUpAddress.Text;
            updateClient.ClientCompanyName = tbClientCompanyName.Text;
            updateClient.ClientContactNo = tbClientContactNo.Text;
            _db.SaveChanges();
            RenterMenu.dataGrid.ItemsSource = _db.Clients.ToList();
            this.Hide();
        }
    }
}
