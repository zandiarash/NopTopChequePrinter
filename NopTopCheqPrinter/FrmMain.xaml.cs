using Stimulsoft.Report;
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

namespace NopTopCheqPrinter
{
    public partial class FrmMain : Window
    {
        //string VarDate = "";
        //long VarMablaghA = 0;
        //string VarMablaghH = "";
        //string VarDarVajh = "";
        //string VarTozihat = "";

        public FrmMain()
        {
            InitializeComponent();
        }
        private void TxtMablaghA_TextChanged(object sender, RoutedEventArgs e)
        {
            //TxtMablaghH.Text= EngineNew.num2str(TxtMablaghA.Text);
            //TxtMablaghH.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, "Cost: {0:N2}", TxtMablaghA.Text.ToString());
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StiReport mainReport = new StiReport();
            var fileRep = RdBankMelli.IsChecked.Value ? "ReportMelli.mrt" : "ReportMellat.mrt";
            mainReport.Load(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "ReportTemplates", fileRep));
            mainReport.CacheAllData = true;
            var dateArr = TxtDate.Text.Trim().Split('/');
            mainReport.Dictionary.Variables["VarDateA"].Value = $"{dateArr[2]}{dateArr[1]}{dateArr[0]}";
            mainReport.Dictionary.Variables["VarDateH"].Value = $"{Engine.GET_Number_To_PersianString(dateArr[0])} {Engine.cMonth(dateArr[1])} {Engine.GET_Number_To_PersianString(dateArr[2])}";
            //mainReport.Dictionary.Variables["VarMablaghA"].Value = TxtMablaghA.Value.ToString().PadLeft(20, '-');
            var parameter = new Stimulsoft.Report.Dictionary.StiVariable("VarMablaghA", Convert.ToInt64(TxtMablaghA.Value));
            mainReport.Dictionary.Variables["VarMablaghA"].Value = parameter;
            mainReport.Dictionary.Variables["VarMablaghH"].Value = TxtMablaghH.Text;
            mainReport.Dictionary.Variables["VarDarVajh"].Value = TxtDarVajh.Text.Trim();
            mainReport.Dictionary.Variables["VarTozihat"].Value = TxtTozihat.Text.Trim();

            if (RdDirectPrint.IsChecked == false)
                mainReport.Show();
            else
                mainReport.Print();
        }
        private void textBoxValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }
        private void textBoxValue_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String Text1 = (String)e.DataObject.GetData(typeof(String));
                if (!TextBoxTextAllowed(Text1)) e.CancelCommand();
            }
            else e.CancelCommand();
        }
        private Boolean TextBoxTextAllowed(String Text2)
        {
            return Array.TrueForAll<Char>(Text2.ToCharArray(), delegate (Char c)
            {
                return Char.IsDigit(c) || Char.IsControl(c);
            });
        }
        private void NumericTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (true || e.Key == Key.Decimal)
            {
                var txb = sender as TextBox;
                int caretPos = txb.CaretIndex;
                txb.Text = txb.Text.Insert(txb.CaretIndex, System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
                txb.CaretIndex = caretPos + 1;
                e.Handled = true;
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TxtDate.Focus();
            this.Title = $"نرم افزار چاپ چک نسخه {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()} (آزمایشی)";
        }


        private void TxtMablaghA_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!string.IsNullOrEmpty(TxtMablaghA.Text))
                TxtMablaghH.Text = EngineNew.num2str(EngineNew.GetIntPart(TxtMablaghA.Text).ToString());
        }

        private void TxtMablaghA_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtMablaghA.Text = TxtMablaghA.Text.Replace("ریا", "").Replace("ل", "");
        }
    }
}
