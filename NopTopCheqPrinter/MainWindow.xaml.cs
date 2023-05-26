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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
                this.WindowState = System.Windows.WindowState.Maximized;
            else
                this.WindowState = System.Windows.WindowState.Normal;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StiReport mainReport = new StiReport();
            mainReport.Load(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "ReportTemplates", "Report.mrt"));
            mainReport.CacheAllData = true;
            mainReport.Dictionary.Variables["Variable"].Value = "aa";
            mainReport.Show();
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
    }
}
