using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReceiptPrinter
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btn_Printer_Clicked(object sender, EventArgs e)
        {
            // Variables
            string ipAddress = "192.168.1.200";
            int portNumber = 9100;

            List<string> myText = new List<string>()
            {
                "Hello", "From", "Moussa", "Please",
                "Like", "Me"
            };

            // Try to find platform specific services
            var printer = DependencyService.Get<ReceiptPrinter.IPrinter>();
            
            if (printer == null)
            {
                // Do not proceed if no services found for the platform
                await DisplayAlert("Error", "No implementation provided for this platform", "Ok");
                return;
            }

            try
            {
                // Call the method, declare by the IPrinter interface
                printer.Print(ipAddress, portNumber, myText);
            }
            catch(Exception ex)
            {
                // Exception here could mean diffeculties in connectin to the printer etc
                await DisplayAlert("Error", $"Failed to print redemption stp\nReason: {ex.Message}", "Ok");
            }
        }
    }
}
