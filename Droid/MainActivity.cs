using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Linq;
using Android.Provider;

namespace Obedear.Droid
{
	[Activity (Label = "Obedear", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		public static List<string> PhoneNumbers { get; set;}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			try
			{
				PhoneNumbers = new List<string>();

				var managedCursor = ManagedQuery(CallLog.Calls.ContentUri, null, null,null, null);

				int number = managedCursor.GetColumnIndex( CallLog.Calls.Number ); 
				while (managedCursor.MoveToNext()) 
				{
					var phoneNumber = managedCursor.GetString(number);
					PhoneNumbers.Insert(0, phoneNumber);
				}

				managedCursor.Close();			
			}
			catch(Exception e) {
			}

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
		}
	}
}

