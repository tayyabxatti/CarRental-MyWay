using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.codec;
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
using System.Windows.Shapes;
using System.Drawing.Imaging;


namespace CarRent.View.Agreementss
{
    /// <summary>
    /// Interaction logic for AgreementUpdate.xaml
    /// </summary>
    public partial class AgreementUpdate : Window
    {
        carRentEntities _db = new carRentEntities();
        int Id;
        public AgreementUpdate(int agreeId)
        {
            InitializeComponent();
            Id = agreeId;
            Load();
        }
        public void Load()
        {
            //like update 
            var views = (from r in _db.RentalAgreements where r.RentalAgreementId == Id select r).SingleOrDefault();
            var kmpkr = views.Reservation.Car.TotalKm * views.Reservation.Car.KmBill;
            var kpkr = views.Reservation.Car.TotalTime * views.Reservation.Car.TimeBill;
            tbInvoiceNo.Text = views.RentalAgreementId.ToString();
            tbBillingAddress.Text = views.Reservation.BillingAddress;
            tbCarName.Text = views.Reservation.Car.CarMake;
            tbCarRegistrationNo.Text = views.Reservation.Car.CarRegistrationNo;
            tbClientContactNo.Text = views.Reservation.Client.ClientContactNo;
            tbClientName.Text = views.Reservation.Client.ClientName;
            tbDriverName.Text = views.Reservation.Driver.DriverName.ToString();
            tbHr.Text = views.Reservation.Car.TotalTime.ToString();
            tbKms.Text = views.Reservation.Car.TotalKm.ToString();
            tbkmsRs.Text = kmpkr.ToString();
            tbhrRs.Text = kpkr.ToString();
            //DriverCharges
            tbGst.Text = views.GST.ToString();
            tbActualItienrary.Text = views.AcutalItinerary;
            tbAmountDue.Text = views.AmountDue.ToString();
            tbFuel.Text = views.AgreementFuel.ToString();
            tbGrandTotal.Text = views.TotalCharges.ToString();
            tbActualItienrary.Text = views.AcutalItinerary.ToString();
            tbToolTax.Text = views.TollTaxCharges.ToString();
            tbDriverNight.Text = views.DriverCharges.ToString();
            tbPrepayment.Text = views.PrePaymen.ToString();
            if (views.FuelOut == "Full")
            {
                FuelStateOutHalf.IsChecked = false;
                FuelStateOutFull.IsChecked = true;
                FuelStateOutQuarter.IsChecked = false;
                FuelStateOutEmpty.IsChecked = false;
            }
            else if (views.FuelOut == "Empty")
            {
                FuelStateOutHalf.IsChecked = false;
                FuelStateOutFull.IsChecked = false;
                FuelStateOutQuarter.IsChecked = false;
                FuelStateOutEmpty.IsChecked = true;
            }
            else if (views.FuelOut == "Quarter")
            {
                FuelStateOutHalf.IsChecked = false;
                FuelStateOutFull.IsChecked = false;
                FuelStateOutEmpty.IsChecked = false;
                FuelStateOutQuarter.IsChecked = true;
            }
            else
            {
                FuelStateOutHalf.IsChecked = true;
                FuelStateOutFull.IsChecked = false;
                FuelStateOutQuarter.IsChecked = false;
                FuelStateOutEmpty.IsChecked = false;
            }
            if (views.Reservation.MethodOfPayment == "Cash")
            {
                tbMethodOdPaymentCash.IsChecked = true;
                tbMehodOfPaymentCredit.IsChecked = false;
            }
            else
            {
                tbMethodOdPaymentCash.IsChecked = false;
                tbMehodOfPaymentCredit.IsChecked = true;
            }
            if (views.Reservation.Car.CarFuelState == "Full")
            {
                cbFuelStateFull.IsChecked = true;
                cbFuelStateHalf.IsChecked = false;
                cbFuelStateQuarter.IsChecked = false;
                cbFuelStateEmpty.IsChecked = false;
            }
            else if (views.Reservation.Car.CarFuelState == "Quarter")
            {
                cbFuelStateFull.IsChecked = false;
                cbFuelStateHalf.IsChecked = false;
                cbFuelStateQuarter.IsChecked = true;
                cbFuelStateEmpty.IsChecked = false;
            }
            else if (views.Reservation.Car.CarFuelState == "Half")
            {
                cbFuelStateFull.IsChecked = false;
                cbFuelStateHalf.IsChecked = true;
                cbFuelStateQuarter.IsChecked = false;
                cbFuelStateEmpty.IsChecked = false;
            }
            else
            {
                cbFuelStateFull.IsChecked = false;
                cbFuelStateHalf.IsChecked = false;
                cbFuelStateQuarter.IsChecked = false;
                cbFuelStateEmpty.IsChecked = true;
            }
            tbPickUpAddressOrFlightNo.Text = views.Reservation.Client.ClientPickUpAddress.ToString();
            tbReservationDateTime.Text = views.Reservation.ReservationDateTime.ToString();
            tbReservationNo.Text = views.Reservation.ReservationId.ToString();
            if (views.AgreementClosed != "Closed")
            {
                tbTimeIn.Text = views.Reservation.Car.TImeIn.ToString();
                tbTimeOut.Text = views.Reservation.Car.TimeOut.ToString();
                tbTimeUsed.Text = views.Reservation.Car.TotalTime.ToString();
                tbDateIn.Text = views.Reservation.Car.DateIn.ToString();
                tbDateOut.Text = views.Reservation.Car.DateOut.ToString();
                tbKmsDriven.Text = views.Reservation.Car.TotalKm.ToString();
                tbKmsIn.Text = views.Reservation.Car.CarKmIn.ToString();
                tbKmsOut.Text = views.Reservation.Car.CarKmOut.ToString();
            }
            else
            {
                tbTimeIn.Text = views.AgreementTimeIn.ToString();
                tbTimeOut.Text = views.AgreementTimeOut.ToString();
                tbTimeUsed.Text = views.AgreementTotalTime.ToString();
                tbDateIn.Text = views.AgreementDateIn.ToString();
                tbDateOut.Text = views.AgreementDateOut.ToString();
                tbKmsDriven.Text = views.AgreementTotalKm.ToString();
                tbKmsIn.Text = views.AgreementKmIn.ToString();
                tbKmsOut.Text = views.AgreementKmOut.ToString();
                
            }

        }

        private void BtnCalculateCharges_Click(object sender, RoutedEventArgs e)
        {
            if (tbFuel.Text == "")
            {
                tbFuel.Text = "0";
            }
            if (tbDriverNight.Text == "")
            {
                tbDriverNight.Text = "0";
            }
            if (tbPrepayment.Text == "")
            {
                tbPrepayment.Text = "0";
            }
            if (tbGst.Text == "")
            {
                tbGst.Text = "0";
            }
            if (tbToolTax.Text == "")
            {
                tbToolTax.Text = "0";
            }
            if (tbAmountDue.Text == "")
            {
                tbAmountDue.Text = "0";
            }
            if (tbDailyCharges.Text == "")
            {
                if(tbMonthlyCharges.Text != "" || tbDailyCharges.Text!="" || tbhrRs.Text== "")
                { 
                int subTotal = Convert.ToInt32(tbMonthlyCharges.Text) + Convert.ToInt32(tbhrRs.Text) + Convert.ToInt32(tbkmsRs.Text);
                var gst = Convert.ToInt32(tbSubTotal.Text) * 16 / 100;
                tbGst.Text = gst.ToString();
                int total = Convert.ToInt32(tbFuel.Text) + Convert.ToInt32(tbToolTax.Text) + Convert.ToInt32(tbDriverNight.Text);
                int grandTotal = Convert.ToInt32(tbPrepayment.Text) + Convert.ToInt32(tbAmountDue.Text);
                tbSubTotal.Text = subTotal.ToString();
                tbTotal.Text = (subTotal + total).ToString();
                tbGrandTotal.Text = (subTotal + total + grandTotal).ToString();
                }
                else
                {
                    MessageBox.Show("you must eneter daily or monthly charges to calculate");
                }
            }
            else if (tbMonthlyCharges.Text == "")
            {
                int subTotal = Convert.ToInt32(tbDailyCharges.Text) + Convert.ToInt32(tbhrRs.Text) + Convert.ToInt32(tbkmsRs.Text);
                var gst = Convert.ToInt32(tbSubTotal.Text) * 16 / 100;
                tbGst.Text = gst.ToString();
                int total = Convert.ToInt32(tbFuel.Text) + Convert.ToInt32(tbToolTax.Text) + Convert.ToInt32(tbDriverNight.Text) + Convert.ToInt32(tbGst.Text);
                int grandTotal = Convert.ToInt32(tbPrepayment.Text) + Convert.ToInt32(tbAmountDue.Text);
                tbSubTotal.Text = subTotal.ToString();
                tbTotal.Text = (subTotal + total).ToString();
                tbGrandTotal.Text = (subTotal + total + grandTotal).ToString();
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            var views = (from r in _db.RentalAgreements where r.RentalAgreementId == Id select r).SingleOrDefault();


            var PdfDOcument = new Document(PageSize.A4, 40f, 40f, 60f, 60f);
            string path = $"d:\\name.pdf";
            PdfWriter.GetInstance(PdfDOcument, new FileStream(path, FileMode.OpenOrCreate));
            PdfDOcument.Open();
            //var imagePath = "";
            var spaceer = new iTextSharp.text.Paragraph("")
            {
                SpacingBefore = 10f,
                SpacingAfter = 10f,
            };
            PdfDOcument.Add(spaceer);

            var image = System.Drawing.Image.FromFile($"d:\\wingspdfdoc.png");
            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Png);
            PdfDOcument.Add(pic);

            var table = new PdfPTable(new[] { .75f, 2f })
            {
                HorizontalAlignment = Convert.ToInt32(Left),
                WidthPercentage = 45,
                DefaultCell = { MinimumHeight = 22f }

            };

            var table1 = new PdfPTable(new[] { .75f, 2f })
            {
                HorizontalAlignment = 2,
                WidthPercentage = 45,
                DefaultCell = { MinimumHeight = 22f }

            };
            var table2 = new PdfPTable(3)
            {
                HorizontalAlignment = 2,
                WidthPercentage = 45,
                DefaultCell = { MinimumHeight = 22f }

            };

            var table3 = new PdfPTable(2);
            table3.AddCell("CAR MAKE/MODEl");
            table3.AddCell(views.Reservation.Car.CarMake);
            table3.AddCell("CAR REG NO");
            table3.AddCell(views.Reservation.Car.CarRegistrationNo);
            var table4 = new PdfPTable(2);
            table4.AddCell("CHAUFFEUR DRIVER");
            table4.AddCell(views.Reservation.Driver.DriverName);
            table4.AddCell("RESERVATION TIME/DATE");
            table4.AddCell(views.Reservation.ReservationDateTime.ToString());
            table1.AddCell(table3);
            table1.AddCell(table4);
            table.AddCell(table1);
            table1.AddCell("RSVN#");
            table1.AddCell(views.Reservation.ReservationId.ToString());
            table1.AddCell("CLIENT NAME");
            table1.AddCell(views.Reservation.Client.ClientName);
            table1.AddCell("MOBILE NO");
            table1.AddCell(views.Reservation.Client.ClientContactNo);
            table1.AddCell("BILLING ADDRESS");
            table1.AddCell(views.Reservation.BillingAddress);
            table1.AddCell("BILLING INSTRUCTION");
            //checkbox billing address
            table1.AddCell("////");
            //checkbox fuel position in and out
            table1.AddCell("FUEL POSITION IN ");
            table1.AddCell("////");
            table1.AddCell("FUEL POSITION OUT");
            table1.AddCell("////");
            table1.AddCell("PICKUP ADDRESS/FLIGHT NO");
            table1.AddCell(views.Reservation.Client.ClientPickUpAddress);
            table1.AddCell("Actual Itenary");
            table.AddCell(views.AcutalItinerary);

            var table5 = new PdfPTable(2);
            table5.AddCell("KMOUT");
            table5.AddCell(views.Reservation.Car.CarKmOut.ToString());
            table5.AddCell("KMIN");
            table5.AddCell(views.Reservation.Car.CarKmIn.ToString());
            table5.AddCell("KM DRIVEN");
            table5.AddCell(views.Reservation.Car.TotalKm.ToString());
            var table6 = new PdfPTable(2);
            table6.AddCell("TIMEOUT");
            table6.AddCell(views.Reservation.Car.TimeOut);
            table6.AddCell("TIMEIN");
            table6.AddCell(views.Reservation.Car.TImeIn);
            table6.AddCell("HOURS USED");
            table6.AddCell(views.Reservation.Car.TotalTime.ToString());
            var table7 = new PdfPTable(2);
            table7.AddCell("DATEOUT");
            table7.AddCell(views.Reservation.Car.DateOut);
            table7.AddCell("DATEIN");
            table7.AddCell(views.Reservation.Car.DateIn);
            table7.AddCell("TOTAL DAYS");
            table7.AddCell(tbTotalDays.Text);


            table2.AddCell(table5);
            table2.AddCell(table6);
            table2.AddCell(table7);
            table2.AddCell("BASIC CHARGES: ");
            table2.AddCell("");
            table2.AddCell("");
            table2.AddCell("DAILY");
            table2.AddCell("///");
            table2.AddCell("");
            table2.AddCell("MONTHLY");
            table2.AddCell("////");
            table2.AddCell("");
            table2.AddCell(views.AgreementTotalTime.ToString());
            table2.AddCell("HR @ RS");
            table2.AddCell(views.HPkr.ToString());
            table2.AddCell(views.AgreementTotalKm.ToString());
            table2.AddCell("KM @ RS");
            table2.AddCell(views.KPkr.ToString());
            table2.AddCell("//");
            table2.AddCell("DRIVER @ RS");
            table2.AddCell("// to be added in db");
            table2.AddCell("16% GST");
            table2.AddCell(views.GST.ToString());
            table2.AddCell("");
            table2.AddCell("FUEL CHARGES");
            table2.AddCell(views.AgreementFuel.ToString());
            table2.AddCell("");
            table2.AddCell("TOOL TAX");
            table2.AddCell(views.TollTaxCharges.ToString());
            table2.AddCell("");
            table2.AddCell("DRIVER NIGHT");
            table2.AddCell(views.DriverCharges.ToString());
            table2.AddCell("");
            table2.AddCell("PREPAYMENT");
            table2.AddCell(views.PrePaymen.ToString());
            table2.AddCell("");
            table2.AddCell("AMOUNT DUE");
            table2.AddCell(views.AmountDue.ToString());
            table2.AddCell("");
            table2.AddCell("TOTAL CHARGES");
            table2.AddCell(views.TotalCharges.ToString());
            table2.AddCell("");
            table.AddCell(table2);

            

            table.AddCell(table2);
            PdfDOcument.Add(table1);
            PdfDOcument.Add(table2);
           
            PdfDOcument.Add(spaceer);
            PdfDOcument.Add(table);
            PdfDOcument.OpenDocument();
            PdfDOcument.Close();


            //headerTable.AddCell("CarMake/Model");
            //headerTable.AddCell(views.Reservation.Car.CarMake);
            //headerTable.AddCell("Car Registration No");
            //headerTable.AddCell(views.Reservation.Car.CarRegistrationNo);
            //headerTable.AddCell("Chauffeur Driver");
            //headerTable.AddCell(views.Reservation.Driver.DriverName);
            //headerTable.AddCell("Reservation No");
            //headerTable.AddCell(views.Reservation.ReservationId.ToString());
            //headerTable.AddCell("Date And Time of Report");
            //headerTable.AddCell(views.Reservation.ReservationDateTime.ToString());
            //headerTable.AddCell("Clients Name");
            //headerTable.AddCell(views.Reservation.Client.ClientName);
            //headerTable.AddCell("Mobile Number");
            //headerTable.AddCell(views.Reservation.Client.ClientContactNo);
            //headerTable.AddCell("Billing Address");
            //headerTable.AddCell(views.Reservation.BillingAddress);
            //headerTable.AddCell("Fuel Position In");
            //headerTable.AddCell("Full or Empty");
            ////FuelPosition In and Out
            //headerTable.AddCell("Fuel Position out");
            //headerTable.AddCell("Full or Empty");
            //headerTable.AddCell("Pickup Address/ Flight No");
            //headerTable.AddCell(views.Reservation.Client.ClientPickUpAddress);
            //headerTable.AddCell("Actual Itinerary");
            //headerTable.AddCell(views.AcutalItinerary);

            //PdfPTable kmtime = new PdfPTable(2);

            //kmtime.AddCell("KmOut");
            //kmtime.AddCell(views.Reservation.Car.CarKmOut.ToString());
            //kmtime.AddCell("KmIn");
            //kmtime.AddCell(views.Reservation.Car.CarKmIn.ToString());
            //table2.AddCell("TimeOut");
            //table2.AddCell(views.Reservation.Car.TimeOut);
            //table2.AddCell("TimeIn");
            //table2.AddCell(views.Reservation.Car.TImeIn);

            //PdfPTable third = new PdfPTable(2);

            //third.AddCell(headerTable);
            //third.AddCell(table2);
            //PdfDOcument.Add(third);
        }
        private void BtnCloseRental_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to close this Rental?", "Close Rental", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var views = (from r in _db.RentalAgreements where r.RentalAgreementId == Id select r).SingleOrDefault();
                views.AgreementClosed = "Closed";
                views.HPkr = Convert.ToInt32(tbhrRs.Text);
                views.KPkr = Convert.ToInt32(tbkmsRs.Text);
                views.GST = Convert.ToInt32(tbGst.Text);
                views.AgreementFuel = Convert.ToInt32(tbFuel.Text);
                views.TollTaxCharges = Convert.ToInt32(tbToolTax.Text);
                views.DriverCharges = Convert.ToInt32(tbDriverNight.Text);
                views.PrePaymen = Convert.ToInt32(tbPrepayment.Text);
                views.AmountDue = Convert.ToInt32(tbAmountDue.Text);
                views.TotalCharges = Convert.ToInt32(tbGrandTotal.Text);
                views.AcutalItinerary = tbActualItienrary.Text;
                views.AgreementDateIn = tbDateIn.Text;
                views.AgreementDateOut = tbDateOut.Text;
                views.AgreementTimeIn = tbTimeIn.Text;
                views.AgreementTimeOut = tbTimeOut.Text;
                views.AgreementKmOut = Convert.ToInt32(tbKmsOut.Text);
                views.AgreementKmIn = Convert.ToInt32(tbKmsIn.Text);
                views.AgreementTotalKm = Convert.ToInt32(tbKms.Text);
                views.AgreementTotalTime = Convert.ToInt32(tbHr.Text);
                views.Reservation.Car.CarKmIn = Convert.ToInt32(tbKmsOut.Text);
                views.Reservation.Car.DateIn = tbDateOut.Text;
                views.Reservation.Car.TImeIn = tbTimeOut.Text;
                if (FuelStateOutFull.IsChecked==true)
                {
                    views.Reservation.Car.CarFuelState = "Full";
                }
                else if (FuelStateOutEmpty.IsChecked == true)
                {
                    views.Reservation.Car.CarFuelState = "Empty";
                }
                else if (FuelStateOutHalf.IsChecked == true)
                {
                    views.Reservation.Car.CarFuelState = "Half";
                }
                else
                {
                    views.Reservation.Car.CarFuelState = "Quarter";
                }
                _db.SaveChanges();
                AgreementList.dataGrid.ItemsSource = _db.RentalAgreements.ToList();
                this.Hide();
            }
            
        }

        private void TbHr_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            tbHr.Text = tbTimeUsed.Text;

        }
        private void TbKms_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            tbKms.Text = tbKmsDriven.Text;
        }
        private void TbKmsDriven_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var views = (from r in _db.RentalAgreements where r.RentalAgreementId == Id select r).SingleOrDefault();
            var totalKm = Int32.Parse(tbKmsIn.Text) - Int32.Parse(tbKmsOut.Text);
            tbKmsDriven.Text = totalKm.ToString();
        }
        private void TbTimeUsed_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var totalTime = DateTime.Parse(tbTimeIn.Text) - DateTime.Parse(tbTimeOut.Text);
            tbTimeUsed.Text = totalTime.Hours.ToString();
        }
        private void TbTotalDays_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var totalDays =  DateTime.Parse(tbDateIn.Text)- DateTime.Parse(tbDateOut.Text);
            tbTotalDays.Text = totalDays.Days.ToString();
        }

        private void TbGst_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var gst = Convert.ToInt32(tbSubTotal.Text) * 16 / 100;
            tbGst.Text = gst.ToString();
        }
    }
}
