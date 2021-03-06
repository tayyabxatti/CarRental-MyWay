﻿using CarRent.View.Agreementss;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ReservationList.xaml
    /// </summary>
    public partial class ReservationList : UserControl
    {
        carRentEntities _db = new carRentEntities();
        public static DataGrid dataGrid;

        public ReservationList()
        {
            InitializeComponent();
            Load();
        }

        public void Load()
        {
            ReservationGrid.ItemsSource = _db.Reservations.ToList();
            dataGrid = ReservationGrid;
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to Delete this Reservation. The Rental Agreement of this reservation will also be deleted?", "Confirm Delete", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {

                int Id = (ReservationGrid.SelectedItem as Reservation).ReservationId;
                var deleteReservation = _db.Reservations.Where(c => c.ReservationId == Id).SingleOrDefault();
                var deleteRental = _db.RentalAgreements.Where(c => c.ReservationId == deleteReservation.ReservationId).SingleOrDefault();
                
                _db.RentalAgreements.Remove(deleteRental);
                _db.Reservations.Remove(deleteReservation);
                _db.SaveChanges();
                ReservationGrid.ItemsSource = _db.Reservations.ToList();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int Id = (ReservationGrid.SelectedItem as Reservation).ReservationId;
            Reserve updateReservation = new Reserve(Id);
            updateReservation.ShowDialog();
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            Reserve reserve = new Reserve(0);
            reserve.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbsearchBookedAt == null)
            {
                var source = _db.Reservations.Where(x => x.Source == tbsearchSource.Text).ToList();
                ReservationGrid.ItemsSource = source;
            }
            else
            {
                var reservationdate = _db.Reservations.Where(x => x.BookedAt == DateTime.Parse(tbsearchBookedAt.Text)).ToList();
                ReservationGrid.ItemsSource = reservationdate;
            }
            if(tbsearchBookedAt != null && tbsearchSource != null)
            {
                var search = _db.Reservations.Where(x => x.BookedAt == DateTime.Parse(tbsearchBookedAt.Text) && x.Source == tbsearchSource.Text).ToList();
                ReservationGrid.ItemsSource = search;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            ReservationGrid.ItemsSource = _db.Reservations.ToList();

        }
        private void TbnPrint_Click(object sender, RoutedEventArgs e)
        {
            int Id = (ReservationGrid.SelectedItem as Reservation).ReservationId;
            var views = _db.Reservations.Where(x => x.ReservationId == Id).SingleOrDefault();
            string path1 = @"D:\Reservation";
           // Create directory temp1 if it doesn't exist
            Directory.CreateDirectory(path1);
            var PdfDOcument = new Document(PageSize.A4, 40f, 40f, 60f, 60f);
            string path = @"D:\Reservation\ReservationNo" +views.ReservationId+".pdf";
            PdfWriter.GetInstance(PdfDOcument, new FileStream(path, FileMode.OpenOrCreate));
            PdfDOcument.Open();
            //var imagePath = "";
            var spaceer = new iTextSharp.text.Paragraph("RESERVATION")
            {
                SpacingBefore = 10f,
                SpacingAfter = 10f,
            };
            PdfDOcument.Add(spaceer);

            var image = System.Drawing.Image.FromFile($"d:\\wingspdfdoc.png");
            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Png);
            PdfDOcument.Add(pic);

            var table = new PdfPTable(2)
            {
                HorizontalAlignment = 1,
                WidthPercentage = 100,
                DefaultCell = { MinimumHeight = 22f }

            };

            table.AddCell("RENTING STATION");
            table.AddCell(views.RentingStation);
            table.AddCell("BOOKED AT");
            table.AddCell(views.BookedAt.ToString());
            table.AddCell("CAR MAKE/MODEL");
            table.AddCell(views.Car.CarMake);
            table.AddCell("DRIVER");
            table.AddCell(views.Driver.DriverName);
            table.AddCell("CHECK IN STATION");
            table.AddCell(views.CheckInStation);
            table.AddCell("RENTER NAME");
            table.AddCell(views.Client.ClientName);
            table.AddCell("PICKUP ADDRESS/FLIGHT NO");
            table.AddCell(views.Client.ClientPickUpAddress);
            table.AddCell("METHOD OF PAYMENT");
            table.AddCell("///");
            table.AddCell("SOURCE");
            table.AddCell(views.Source);
            table.AddCell("TELEPHONE CONTACT");
            table.AddCell(views.Client.ClientContactNo);
            table.AddCell("STAFF NAME");
            table.AddCell(views.Staff.StaffName);
            table.AddCell("NOTE");
            table.AddCell(views.Note);
            PdfDOcument.Add(table);
            PdfDOcument.OpenDocument();
            PdfDOcument.Close();
        }

        private void BtnRental_Click(object sender, RoutedEventArgs e)
        {
            int Id = (ReservationGrid.SelectedItem as Reservation).ReservationId;
            var rentals = _db.RentalAgreements.Where(x => x.ReservationId == Id).Select(a=> a.RentalAgreementId).SingleOrDefault();
            
            AgreementUpdate agreementUpdate = new AgreementUpdate(rentals);
            agreementUpdate.ShowDialog();
        }
    }
}
