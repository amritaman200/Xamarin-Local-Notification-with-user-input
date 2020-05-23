using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalNotification
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        string response;
       


        public MainPage()
        {
            InitializeComponent();
            MessagingCenter.Unsubscribe<string, string>("this", "iosAlertResponse");
            MessagingCenter.Subscribe<string, string>("this", "iosAlertResponse", (args, email) =>
            {
                responseLabel.Text = "Response is : " + email;
            });
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {

            response = DependencyService.Get<IDependencyService>().SendNotification();
            if (response != null)
            {
                
            }
        }
    }
}
