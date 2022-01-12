using Android.Content;
using RolgordijnApp;
using RolgordijnApp.Message;
using Xamarin.Forms;

namespace RolgordijnApp.Droid.Alarm
{
    [BroadcastReceiver(Enabled = true, Label = "Local Notifications Broadcast Receiver")]
    public class AlarmHandler : BroadcastReceiver
    {
        public string LdrStatusText { get; private set; }

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
                string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);

                AndroidNotificationManager manager = AndroidNotificationManager.Instance ?? new AndroidNotificationManager();
                manager.Show(title, message);

                try
                {
                    Client.Send("<ROLLUP>");
                }
                catch
                {
                }
            }
        }
    }
}