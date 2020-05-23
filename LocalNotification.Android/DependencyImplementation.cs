using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(LocalNotification.Droid.DependencyImplementation))]
namespace LocalNotification.Droid
{
    public class DependencyImplementation : IDependencyService
    {
        private Context mContext;
        private NotificationManager mNotificationManager;
        private NotificationCompat.Builder mBuilder;
        public static String NOTIFICATION_CHANNEL_ID = "10023";

        public DependencyImplementation()
        {
            mContext = global::Android.App.Application.Context;
        }
        public string SendNotification()
        {
            try
            {
                var intent = new Intent(mContext, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                intent.PutExtra("Android Notification", "Notificaiton");
                var pendingIntent = PendingIntent.GetActivity(mContext, 0, intent, PendingIntentFlags.OneShot);

                var sound = global::Android.Net.Uri.Parse(ContentResolver.SchemeAndroidResource + "://" + mContext.PackageName + "/" + Resource.Attribute.actionBarDivider);
                // Creating an Audio Attribute
                var alarmAttributes = new AudioAttributes.Builder()
                    .SetContentType(AudioContentType.Sonification)
                    .SetUsage(AudioUsageKind.Notification).Build();

                mBuilder = new NotificationCompat.Builder(mContext);
                mBuilder.SetSmallIcon(Resource.Drawable.alerticon);
                mBuilder.SetContentTitle("Android Notification")
                        .SetSound(sound)
                        .SetAutoCancel(true)
                        .SetContentTitle("Android Notification")
                        .SetContentText("Notificaiton")
                        .SetChannelId(NOTIFICATION_CHANNEL_ID)
                        .SetPriority((int)NotificationPriority.High)
                        .SetVibrate(new long[0])
                        .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate)
                        .SetVisibility((int)NotificationVisibility.Public)
                        .SetSmallIcon(Resource.Drawable.alerticon)
                        .SetContentIntent(pendingIntent);



                NotificationManager notificationManager = mContext.GetSystemService(Context.NotificationService) as NotificationManager;

                if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
                {
                    NotificationImportance importance = global::Android.App.NotificationImportance.High;

                    NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, "Notificaiton", importance);
                    notificationChannel.EnableLights(true);
                    notificationChannel.EnableVibration(true);
                    notificationChannel.SetSound(sound, alarmAttributes);
                    notificationChannel.SetShowBadge(true);
                    notificationChannel.Importance = NotificationImportance.High;
                    notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

                    if (notificationManager != null)
                    {
                        mBuilder.SetChannelId(NOTIFICATION_CHANNEL_ID);
                        notificationManager.CreateNotificationChannel(notificationChannel);
                    }
                }

                notificationManager.Notify(0, mBuilder.Build());
                //MainActivity.ShowAlert();

                
                AlertDialog.Builder alertDiag = new AlertDialog.Builder(MainActivity.contextForDialog);
                alertDiag.SetTitle("Notification");
                alertDiag.SetMessage("Notification Generated");

                EditText input = new EditText(MainActivity.contextForDialog);
                // Specify the type of input expected; this, for example, sets the input as a password, and will mask the text
                input.SetRawInputType(Android.Text.InputTypes.ClassText | Android.Text.InputTypes.NumberVariationPassword);
                alertDiag.SetView(input);

                alertDiag.SetPositiveButton("OK", (senderAlert, args) => {

                    sendValue(input.Text.ToString());

                    //Toast.MakeText(MainActivity.contextForDialog, "Notification Opened", ToastLength.Short).Show();
                });
                alertDiag.SetNegativeButton("Cancel", (senderAlert, args) => {
                    alertDiag.Dispose();
                });
              Dialog diag  = alertDiag.Create();
                diag.Show();
            }
            catch (Exception ex)
            {
                //
            }
            return "123321";
        }

        private void sendValue(string args)
        {
            var value = args;
            MessagingCenter.Send<string, string>("this", "iosAlertResponse", args.ToString());
        }

        public string GetResponse()
        {
           
            return "123321";
        }
    }
}
