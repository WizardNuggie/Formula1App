using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Firebase.Messaging;
using Microsoft.Maui.Platform;

namespace YourApp.Platforms.Android.Services
{
    [Service(Name = "yourapp.MyFirebaseMessagingService", Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "FCM";

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            Log.Debug(TAG, $"Message received from: {message.From}");
            var body = message.GetNotification()?.Body;
            var title = message.GetNotification()?.Title;

            ShowNotification(title, body);
        }

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            Log.Debug(TAG, $"FCM token: {token}");
            // Send token to your backend here if needed
        }

        void ShowNotification(string title, string message)
        {
            var notificationManager = NotificationManager.FromContext(this);

            var channelId = "default_channel_id";
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(channelId, "Default Channel", NotificationImportance.Default);
                notificationManager.CreateNotificationChannel(channel);
            }

            
            var notification = new Notification.Builder(this, channelId)
                .SetContentTitle(title)
                .SetContentText(message)
                .Build();

            notificationManager.Notify(0, notification);
        }
    }
}
