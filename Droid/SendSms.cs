using System;
using Android.Content;
using Obedear.Droid;
using System.Collections.Generic;
using System.Linq;

[assembly: Xamarin.Forms.Dependency (typeof (SendSms_Android))]
namespace Obedear.Droid
{
	public class SendSms_Android : ISendSms
	{
		public SendSms_Android()
		{
		}
		
		public void Send(string phone, string sms)
		{
			var smsUri = Android.Net.Uri.Parse("smsto:" + phone);
			var smsIntent = new Intent (Intent.ActionSendto, smsUri);
			smsIntent.PutExtra ("sms_body", sms);  
			Xamarin.Forms.Forms.Context.StartActivity (smsIntent);
		}

		public List<string> GetCalls()
		{
			return MainActivity.PhoneNumbers;
		}
	}
}

