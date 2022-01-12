using Android.App;
using Android.Widget;
using RolgordijnApp.Droid.Message;
using RolgordijnApp.Message;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidMessage))]
namespace RolgordijnApp.Droid.Message
{
    public class AndroidMessage : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long);
        }
        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}