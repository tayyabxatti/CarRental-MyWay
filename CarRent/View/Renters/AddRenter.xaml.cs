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
    /// Interaction logic for AddRenter.xaml
    /// </summary>
    public partial class AddRenter : Window
    {
        carRentEntities _db = new carRentEntities();
        int Id;
        public AddRenter(int RenterId)
        {
            InitializeComponent();
            Id = RenterId;
            LoadorInsert();

        }
        public void LoadorInsert()
        {
            if (Id != 0)
            {
                updateLoad();
            }
        }

        public void updateLoad()
        {
            var updateClient = _db.Clients.Where(x => x.ClientId == Id).SingleOrDefault();
            tbClientCompanyName.Text = updateClient.ClientCompanyName;
            tbClientContactNo.Text = updateClient.ClientContactNo;
            tbClientName.Text = updateClient.ClientName;
            tbClientPickUpAddress.Text = updateClient.ClientPickUpAddress;
            tbClientNote.Text = updateClient.ClientNote;
            tbClientEmail.Text = updateClient.ClientEmail;

        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (Id == 0)
            {
                Client client = new Client()
                {
                    ClientName = tbClientName.Text,
                    ClientCompanyName = tbClientCompanyName.Text,
                    ClientContactNo = tbClientContactNo.Text,
                    ClientPickUpAddress = tbClientPickUpAddress.Text,
                    ClientNote = tbClientNote.Text,
                    ClientEmail = tbClientEmail.Text,
                };
                _db.Clients.Add(client);
                _db.SaveChanges();

                RenterMenu.dataGrid.ItemsSource = _db.Clients.ToList();
                this.Hide();
            }
            else
            {
                var updateClient = _db.Clients.Where(d => d.ClientId == Id).SingleOrDefault();
                updateClient.ClientName = tbClientName.Text;
                updateClient.ClientPickUpAddress = tbClientPickUpAddress.Text;
                updateClient.ClientCompanyName = tbClientCompanyName.Text;
                updateClient.ClientContactNo = tbClientContactNo.Text;
                updateClient.ClientNote = tbClientNote.Text;
                updateClient.ClientEmail = tbClientEmail.Text;
                _db.SaveChanges();
                RenterMenu.dataGrid.ItemsSource = _db.Clients.ToList();
                this.Hide();
            }
        }
    }
}
