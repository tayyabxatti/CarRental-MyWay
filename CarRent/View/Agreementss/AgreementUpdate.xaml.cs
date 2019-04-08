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
using System.Text.RegularExpressions;

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
            //var kmpkr = views.Reservation.Car.TotalKm * views.Reservation.Car.KmBill;
            //var kpkr = views.Reservation.Car.TotalTime * views.Reservation.Car.TimeBill;
            tbInvoiceNo.Text = views.RentalAgreementId.ToString();
            tbBillingAddress.Text = views.Reservation.BillingAddress;
            tbCarName.Text = views.Reservation.Car.CarMake;
            tbCarRegistrationNo.Text = views.Reservation.Car.CarRegistrationNo;
            tbClientContactNo.Text = views.Reservation.Client.ClientContactNo;
            tbClientName.Text = views.Reservation.Client.ClientName;
            tbDriverName.Text = views.Reservation.Driver.DriverName.ToString();
            tbHr.Text = views.Reservation.Car.TotalTime.ToString();
            tbKms.Text = views.Reservation.Car.TotalKm.ToString();
            //tbkmsRs.Text = kmpkr.ToString();
            //tbhrRs.Text = kpkr.ToString();
            //DriverCharges
            tbGst.Text = views.GST.ToString();
            tbActualItienrary.Text = views.AcutalItinerary;
            tbAmountDue.Text = views.AmountDue.ToString();
            tbFuel.Text = views.AgreementFuel.ToString();

            tbGrandTotal.Text = views.TotalCharges.ToString();
            tbToolTax.Text = views.TollTaxCharges.ToString();
            tbDriverNight.Text = views.DriverCharges.ToString();
            tbPrepayment.Text = views.PrePayment.ToString();
            tbPickUpAddressOrFlightNo.Text = views.Reservation.Client.ClientPickUpAddress.ToString();
            tbReservationDateTime.Text = views.Reservation.ReservationDateTime.ToString();
            tbReservationNo.Text = views.Reservation.ReservationId.ToString();


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

            if(views.AgreementClosed == "Closed") { 
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
            else if (views.FuelOut == "Half")
            {
                FuelStateOutHalf.IsChecked = true;
                FuelStateOutFull.IsChecked = false;
                FuelStateOutQuarter.IsChecked = false;
                FuelStateOutEmpty.IsChecked = false;
            }

            if (views.FuelIn == "Full")
            {
                cbFuelStateHalf.IsChecked = false;
                cbFuelStateFull.IsChecked = true;
                cbFuelStateQuarter.IsChecked = false;
                cbFuelStateEmpty.IsChecked = false;
            }
            else if (views.FuelIn == "Empty")
            {
                cbFuelStateHalf.IsChecked = false;
                cbFuelStateFull.IsChecked = false;
                cbFuelStateQuarter.IsChecked = false;
                cbFuelStateEmpty.IsChecked = true;
            }
            else if (views.FuelIn == "Quarter")
            {
                cbFuelStateHalf.IsChecked = false;
                cbFuelStateFull.IsChecked = false;
                cbFuelStateQuarter.IsChecked = true;
                cbFuelStateEmpty.IsChecked = false;
            }
            else if (views.FuelIn == "Half")
            {
                cbFuelStateHalf.IsChecked = true;
                cbFuelStateFull.IsChecked = false;
                cbFuelStateQuarter.IsChecked = false;
                cbFuelStateEmpty.IsChecked = false;
            }
        }

            if (views.DailyCharges == null)
            {
                tbMonthlyCharges.Text = views.MonthlyCharges.ToString();
            }
            else if(views.MonthlyCharges == null)
            {
                tbDailyCharges.Text = views.DailyCharges.ToString();
            }

            

            if (views.AgreementClosed != "Closed")
            {

                if (views.Reservation.Car.CarFuelState == "Full")
                {
                    FuelStateOutFull.IsChecked = true;
                    FuelStateOutHalf.IsChecked = false;
                    FuelStateOutQuarter.IsChecked = false;
                    FuelStateOutEmpty.IsChecked = false;
                    views.FuelOut = "Full";
                }
                else if (views.Reservation.Car.CarFuelState == "Quarter")
                {
                    FuelStateOutFull.IsChecked = false;
                    FuelStateOutHalf.IsChecked = false;
                    FuelStateOutQuarter.IsChecked = true;
                    FuelStateOutEmpty.IsChecked = false;
                    views.FuelOut = "Quarter";
                }
                else if (views.Reservation.Car.CarFuelState == "Half")
                {
                    FuelStateOutFull.IsChecked = false;
                    FuelStateOutHalf.IsChecked = true;
                    FuelStateOutQuarter.IsChecked = false;
                    FuelStateOutEmpty.IsChecked = false;
                    views.FuelOut = "Half";
                }
                else if (views.Reservation.Car.CarFuelState == "Empty")
                {
                    FuelStateOutFull.IsChecked = false;
                    FuelStateOutHalf.IsChecked = false;
                    FuelStateOutQuarter.IsChecked = false;
                    FuelStateOutEmpty.IsChecked = true;
                    views.FuelOut = "Empty";
                }


                
                //tbTimeIn.Text = views.Reservation.Car.TimeOut.ToString();
                tbTimeOut.Text = views.Reservation.Car.TImeIn.ToString();
                //tbTimeUsed.Text = views.Reservation.Car.TotalTime.ToString();
                //tbDateIn.Text = views.Reservation.Car.DateIn.ToString();
                tbDateOut.Text = views.Reservation.Car.DateIn.ToString();
                //tbKmsDriven.Text = views.Reservation.Car.TotalKm.ToString();
                //tbKmsIn.Text = views.Reservation.Car.CarKmIn.ToString();
                tbKmsOut.Text = views.Reservation.Car.CarKmIn.ToString();
            }
            else
            {
                var timeUsed = (DateTime.Parse(views.AgreementTimeOut).Hour - DateTime.Parse(views.AgreementTimeIn).Hour);
                var timecharge = Convert.ToInt32(timeUsed) * views.Reservation.Car.TimeBill;

                var kmUsed = views.AgreementKmOut - views.AgreementKmIn;
                var kmcharge = kmUsed * views.Reservation.Car.KmBill;

                tbKms.Text = kmUsed.ToString();
                tbKmsDriven.Text = kmUsed.ToString();
                tbkmsRs.Text = kmcharge.ToString();

                tbTimeUsed.Text = timeUsed.ToString();
                tbHr.Text = timeUsed.ToString();
                tbhrRs.Text = timecharge.ToString();

                tbTimeUsed.Text =timeUsed.ToString();
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
            var views = (from r in _db.RentalAgreements where r.RentalAgreementId == Id select r).SingleOrDefault();

            if (views.AgreementClosed != "Closed")
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
                if (tbSubTotal.Text == "")
                {
                    tbSubTotal.Text = "0";
                }
                if (tbDailyCharges.Text == "")
                {
                    if (tbMonthlyCharges.Text != "" || tbDailyCharges.Text != "" )
                    {
                        int subTotal = Convert.ToInt32(tbMonthlyCharges.Text) + Convert.ToInt32(tbhrRs.Text) + Convert.ToInt32(tbkmsRs.Text);
                        var gst = Convert.ToInt32(tbSubTotal.Text) * 16 / 100;
                        tbGst.Text = gst.ToString();
                        int total = Convert.ToInt32(tbFuel.Text) + Convert.ToInt32(tbToolTax.Text) + Convert.ToInt32(tbDriverNight.Text) + Convert.ToInt32(tbGst.Text);
                        int grandTotal = Convert.ToInt32(tbPrepayment.Text) + Convert.ToInt32(tbAmountDue.Text);
                        tbSubTotal.Text = subTotal.ToString();
                        tbTotal.Text = (total).ToString();
                        tbGrandTotal.Text = (total + grandTotal).ToString();
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
            else
            {
                MessageBox.Show("This Rental is already closed you can't claculate the charges");
            }
        }
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            var views = (from r in _db.RentalAgreements where r.RentalAgreementId == Id select r).SingleOrDefault();


            var PdfDOcument = new Document(PageSize.A4, 40f, 40f, 60f, 60f);

            string path1 = @"D:\RentalAgreements";
            // Create directory temp1 if it doesn't exist
            Directory.CreateDirectory(path1);
            string path = @"D:\RentalAgreements\AgreementNo" + views.RentalAgreementId + ".pdf";

            PdfWriter.GetInstance(PdfDOcument, new FileStream(path, FileMode.OpenOrCreate));
            PdfDOcument.Open();
            //var imagePath = "";
            var spaceer = new iTextSharp.text.Paragraph("")
            {
                SpacingBefore = 10f,
                SpacingAfter = 10f,
            };
            var image = System.Drawing.Image.FromFile($"d:\\wingspdfdoc.png");
            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Png);
            PdfDOcument.Add(pic);
            var table = new PdfPTable(2)
            {
                HorizontalAlignment = Convert.ToInt32(Left),

                WidthPercentage = 45,
                DefaultCell = { MinimumHeight = 22f, }

            };

            var table1 = new PdfPTable(new[] { .75f, 2f })
            {
                HorizontalAlignment = Convert.ToInt32(Left),
                WidthPercentage = 100,
                DefaultCell = { MinimumHeight = 22f }

            };
            var table2 = new PdfPTable(3)
            {
                HorizontalAlignment = 2,
                WidthPercentage = 45,
                ExtendLastRow=false,
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
            table1.AddCell(views.Reservation.BillingAddress.ToString());
            //checkbox fuel position in and out
            table1.AddCell("FUEL POSITION IN ");
            table1.AddCell(views.FuelIn.ToString());
            table1.AddCell("FUEL POSITION OUT");
            table1.AddCell(views.FuelOut.ToString());
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
            table2.AddCell(views.DailyCharges.ToString());
            table2.AddCell("");
            table2.AddCell("MONTHLY");
            table2.AddCell(views.MonthlyCharges.ToString());
            table2.AddCell("");
            table2.AddCell(views.AgreementTotalTime.ToString());
            table2.AddCell("HR @ RS");
            table2.AddCell(views.HPkr.ToString());
            table2.AddCell(views.AgreementTotalKm.ToString());
            table2.AddCell("KM @ RS");
            table2.AddCell(views.KPkr.ToString());
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
            table2.AddCell(views.PrePayment.ToString());
            table2.AddCell("");
            table2.AddCell("AMOUNT DUE");
            table2.AddCell(views.AmountDue.ToString());
            table2.AddCell("");
            table2.AddCell("TOTAL CHARGES");
            table2.AddCell(views.TotalCharges.ToString());
            table2.AddCell("");
            table.AddCell(table2);
            PdfDOcument.Add(table1);
            PdfDOcument.Add(table2);
            PdfDOcument.OpenDocument();
            PdfDOcument.Close();
        }
        private void BtnCloseRental_Click(object sender, RoutedEventArgs e)
        {
            var views = (from r in _db.RentalAgreements where r.RentalAgreementId == Id select r).SingleOrDefault();
            if (Int32.Parse(tbKmsIn.Text) > Int32.Parse(tbKmsOut.Text))
            {
                if (views.AgreementClosed != "Closed")
                {

                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to close this Rental?", "Close Rental", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        views.AgreementClosed = "Closed";
                        views.HPkr = Convert.ToInt32(tbhrRs.Text);
                        views.KPkr = Convert.ToInt32(tbkmsRs.Text);
                        views.GST = Convert.ToInt32(tbGst.Text);
                        views.AgreementFuel = Convert.ToInt32(tbFuel.Text);
                        views.Reservation.Car.CarFuelState = views.AgreementFuel.ToString();
                        views.TollTaxCharges = Convert.ToInt32(tbToolTax.Text);
                        views.DriverCharges = Convert.ToInt32(tbDriverNight.Text);
                        views.PrePayment = Convert.ToInt32(tbPrepayment.Text);
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

                        views.Reservation.Car.CarKmOut = views.Reservation.Car.CarKmIn;
                        views.Reservation.Car.CarKmIn = Convert.ToInt32(tbKmsIn.Text);
                        
                        views.Reservation.Car.TimeOut = views.Reservation.Car.TImeIn;
                        views.Reservation.Car.TImeIn = (tbTimeIn.Text);

                        views.Reservation.Car.DateOut = views.Reservation.Car.DateIn;
                        views.Reservation.Car.DateIn = (tbDateIn.Text);

                        if (views.Reservation.Car.CarFuelState == "Full")
                        {
                            FuelStateOutHalf.IsChecked = false;
                            FuelStateOutQuarter.IsChecked = false;
                            FuelStateOutEmpty.IsChecked = false;
                            FuelStateOutFull.IsChecked = true;
                            views.FuelOut = "Full";
                        }
                        else if (views.Reservation.Car.CarFuelState == "Empty" )
                        {
                            FuelStateOutHalf.IsChecked = false;
                            FuelStateOutQuarter.IsChecked = false;
                            FuelStateOutEmpty.IsChecked = true;
                            FuelStateOutFull.IsChecked = false;
                            views.FuelOut = "Empty";
                        }
                        else if (views.Reservation.Car.CarFuelState == "Half")
                        {
                            FuelStateOutHalf.IsChecked = true;
                            FuelStateOutQuarter.IsChecked = false;
                            FuelStateOutEmpty.IsChecked = false;
                            FuelStateOutFull.IsChecked = false;
                            views.FuelOut = "Half";
                        }
                        else
                        {
                            FuelStateOutHalf.IsChecked = false;
                            FuelStateOutQuarter.IsChecked = true;
                            FuelStateOutEmpty.IsChecked = false;
                            FuelStateOutFull.IsChecked = false;
                            views.FuelOut = "Quarter";
                        }



                        if (cbFuelStateFull.IsChecked == true)
                        {
                            views.Reservation.Car.CarFuelState = "Full";
                            views.FuelIn = "Full";
                        }
                        else if (cbFuelStateEmpty.IsChecked == true)
                        {
                            views.Reservation.Car.CarFuelState = "Empty";
                            views.FuelIn = "Empty";
                        }
                        else if (cbFuelStateHalf.IsChecked == true)
                        {
                            views.Reservation.Car.CarFuelState = "Half";
                            views.FuelIn = "Half";
                        }
                        else
                        {
                            views.Reservation.Car.CarFuelState = "Quarter";
                            views.FuelIn = "Quarter";
                        }
                        if (tbDailyCharges.Text != "")
                        {
                            views.DailyCharges = Convert.ToInt32(tbDailyCharges.Text);
                            views.MonthlyCharges = null;
                        }
                        else if (tbMonthlyCharges.Text != "")
                        {
                            views.MonthlyCharges = Convert.ToInt32(tbMonthlyCharges.Text);
                            views.DailyCharges = null;
                        }
                        _db.SaveChanges();
                        AgreementList.dataGrid.ItemsSource = _db.RentalAgreements.ToList();
                        this.Hide();

                    }


                }
                else
                {
                    MessageBox.Show("This Rental is already closed");
                }
            }
            else
            {
                MessageBox.Show("KmIn must be greater than KmOut");
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
            tbKms.Text = totalKm.ToString();
            var price = Int32.Parse(tbKms.Text) * views.Reservation.Car.KmBill;
            tbkmsRs.Text = price.ToString();
        }
        private void TbTimeUsed_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var views = (from r in _db.RentalAgreements where r.RentalAgreementId == Id select r).SingleOrDefault();
            var totalTime = DateTime.Parse(tbTimeIn.Text) - DateTime.Parse(tbTimeOut.Text);
            //totaldays + time used
            var x = Convert.ToInt32(tbTotalDays.Text) * 24;
            var y = totalTime.Hours + x;
            tbTimeUsed.Text = y.ToString();
            tbHr.Text = tbTimeUsed.Text;
            var timer = Int32.Parse(tbTimeUsed.Text) * views.Reservation.Car.TimeBill;
            tbhrRs.Text = timer.ToString();
        }
        private void TbTotalDays_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var totalDays = DateTime.Parse(tbDateIn.Text) - DateTime.Parse(tbDateOut.Text);
            tbTotalDays.Text = totalDays.Days.ToString();
        }
        private void TbGst_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var gst = Convert.ToInt32(tbSubTotal.Text) * 16 / 100;
            tbGst.Text = gst.ToString();
        }
        private void TbdriverRs_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
        private void FuelStateOutFull_Checked(object sender, RoutedEventArgs e)
        {
            FuelStateOutEmpty.IsChecked = false;
            FuelStateOutFull.IsChecked = true;
            FuelStateOutQuarter.IsChecked = false;
            FuelStateOutHalf.IsChecked = false;
        }
        private void FuelStateOutHalf_Checked(object sender, RoutedEventArgs e)
        {
            FuelStateOutEmpty.IsChecked = false;
            FuelStateOutQuarter.IsChecked = false;
            FuelStateOutHalf.IsChecked = true;
            FuelStateOutFull.IsChecked = false;
        }
        private void FuelStateOutQuarter_Checked(object sender, RoutedEventArgs e)
        {
            FuelStateOutEmpty.IsChecked = false;
            FuelStateOutQuarter.IsChecked = true;
            FuelStateOutHalf.IsChecked = false;
            FuelStateOutFull.IsChecked = false;
        }
        private void FuelStateOutEmpty_Checked(object sender, RoutedEventArgs e)
        {
            FuelStateOutEmpty.IsChecked = true;
            FuelStateOutQuarter.IsChecked =false;
            FuelStateOutHalf.IsChecked = false;
            FuelStateOutFull.IsChecked = false;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CbFuelStateFull_Checked(object sender, RoutedEventArgs e)
        {
            cbFuelStateFull.IsChecked = true;
            cbFuelStateEmpty.IsChecked = false;
            cbFuelStateHalf.IsChecked = false;
            cbFuelStateQuarter.IsChecked = false;
        }

        private void CbFuelStateHalf_Checked(object sender, RoutedEventArgs e)
        {
            cbFuelStateFull.IsChecked = false;
            cbFuelStateEmpty.IsChecked = false;
            cbFuelStateHalf.IsChecked = true;
            cbFuelStateQuarter.IsChecked = false;

        }

        private void CbFuelStateQuarter_Checked(object sender, RoutedEventArgs e)
        {
            cbFuelStateFull.IsChecked = false;
            cbFuelStateEmpty.IsChecked = false;
            cbFuelStateHalf.IsChecked = false;
            cbFuelStateQuarter.IsChecked = true;
        }

        private void CbFuelStateEmpty_Checked(object sender, RoutedEventArgs e)
        {
            cbFuelStateFull.IsChecked = false;
            cbFuelStateEmpty.IsChecked = true;
            cbFuelStateHalf.IsChecked = false;
            cbFuelStateQuarter.IsChecked = false;
        }
    }
}
