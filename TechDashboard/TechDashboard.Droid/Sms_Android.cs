using Android.Telephony;

using TechDashboard.Droid;
using TechDashboard.Services;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(Sms_Android))]
namespace TechDashboard.Droid
{
    public class Sms_Android : ISms
    {
        public Sms_Android()
        {
            // empty
        }

        public void SendSms()
        {
            SmsManager.Default.SendTextMessage("7179409678", null, "Hello from the SMS interface!", null, null);

            //var sendSMSIntent = FindViewById<Button>(Resource.Id.sendSMSIntent);

            //sendSMSIntent.Click += (sender, e) => {
            //    var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
            //    var smsIntent = new Intent(Intent.ActionSendto, smsUri);
            //    smsIntent.PutExtra("sms_body", "hello from Xamarin.Android");
            //    StartActivity(smsIntent);
            //};
        }
    }
}