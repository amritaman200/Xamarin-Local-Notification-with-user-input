using System;
using Foundation;
using LocalNotification;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(LocalNotification.iOS.DependencyImplementation))]
namespace LocalNotification.iOS
{
    public class DependencyImplementation : IDependencyService
    {
        UILocalNotification notification;
        public DependencyImplementation()
        {
            
        }
        public string SendNotification()
        {
            notification = new UILocalNotification();
            notification.FireDate = NSDate.FromTimeIntervalSinceNow(5);
            //notification.AlertTitle = "Alert Title"; // required for Apple Watch notifications
            notification.AlertAction = "View Alert";
            notification.AlertBody = "Your 15 second alert has fired!";
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
            var notify = notification;
            return "123321";
        }
        public string GetResponse()
        {
            return AppDelegate.Email;

        }
       
    }
}
