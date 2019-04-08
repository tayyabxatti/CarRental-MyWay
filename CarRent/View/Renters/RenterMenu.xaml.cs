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

namespace CarRent.View.Renters
{
    /// <summary>
    /// Interaction logic for RenterMenu.xaml
    /// </summary>
    public partial class RenterMenu : UserControl
    {
        carRentEntities _db = new carRentEntities();
        public static DataGrid dataGrid;
        public RenterMenu()
        {
            InitializeComponent();
            Load();
        }
        public void Load()
        {
            ClientGrid.ItemsSource = _db.Clients.ToList();
            dataGrid = ClientGrid;  

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int Id = (ClientGrid.SelectedItem as Client).ClientId;
            AddRenter updateRenter = new AddRenter(Id);
            updateRenter.ShowDialog();

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to Delete this Client?", "Confirm Delete", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                int Id = (ClientGrid.SelectedItem as Client).ClientId;
                var deleteclient = _db.Clients.Where(c => c.ClientId == Id).SingleOrDefault();
                _db.Clients.Remove(deleteclient);
                _db.SaveChanges();
                ClientGrid.ItemsSource = _db.Clients.ToList();
            }
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            AddRenter addRenter = new AddRenter(0);
            addRenter.ShowDialog();

        }
    }

}
