using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;

namespace Obedear
{
	public partial class MainPage : ContentPage
	{
		public DateTime SelectedDate { get; set; }

		public TimeSpan SelectedTime { get; set; }

		public bool IsFemale { get; set; }

		public List<string> Times { get; set; }

		public int SelectedTimeIndex { get; set; }

		public string Name { get; set; }

		public string Phone { get; set; }

		public string Sex { get; set; }

		public Command Send { get; set; }

		private ISendSms _service;

		public MainPage ()
		{
			_service = DependencyService.Get<ISendSms> ();
			var calls = _service.GetCalls ();	

			SelectedDate = DateTime.Now;
			SelectedTime = new TimeSpan (18, 0, 0);

			Send = new Command<string> (SendMessage);

			Times = new List<string> { "16:30", "17:15", "18:00", "18:30", "18:45", "19:15", "19:30" };

			Name = "Name";
			Phone = calls != null && calls.Any() ? calls.First() : "05";
			Sex = "Male";

			BindingContext = this;

			InitializeComponent ();

			foreach(var i in Times)
				timePicker.Items.Add (i);
		}

		private void switchToggled(object sender, EventArgs args)
		{
			Sex = IsFemale ? "Female" : "Male";
			OnPropertyChanged ("Sex");
		}

		private void SendMessage(string param)
		{
			var template = germanyMale;
			if (param == "Germany" && Sex == "Female")
				template = germanyFemale;
			if (param == "Premed" && Sex == "Male")
				template = premedMale;
			if (param == "Premed" && Sex == "Female")
				template = premedFemale;

			var day = GetDay();
			var date = GetDate();

			template = template.Replace("{שם}", Name);
			template = template.Replace("{שעה}", Times[SelectedTimeIndex]);
			template = template.Replace("{תאריך}", date);
			template = template.Replace("{יום בשבוע}", day);

			_service.Send(Phone, template);
		}

		private string GetDate()
		{
			return SelectedDate.ToString("dd.MM");
		}

		private string GetDay()
		{
			switch (SelectedDate.DayOfWeek) {
			case DayOfWeek.Sunday:
				return "ראשון";
			case DayOfWeek.Monday:
				return "שני";
			case DayOfWeek.Tuesday:
				return "שלישי";
			case DayOfWeek.Wednesday:
				return "רביעי";
			case DayOfWeek.Thursday:
				return "חמישי";
			case DayOfWeek.Friday:
				return "שישי";
			case DayOfWeek.Saturday:
				return "שבת";
			}
			throw new Exception ("Ups");
		}

		private const string premedMale = @"שלום {שם},

אתה מוזמן לפגישת יעוץ במכינה לרפואה בחו״ל בהנהלת ד״ר שחר גבע.
הפגישה תתקיים ביום {יום בשבוע} ה- {תאריך} בשעה {שעה}, במשרדי בית הספר שברחוב יגאל אלון 123, תל אביב.
הכניסה לבניין מרחוב מוזס.
קוד כניסה לבניין 4545#.

מומלץ להגיע לפגישה בליווי ההורים.

בברכה,
ד״ר נעמי טרושינה

יועצת אקדמית,
המכינה לרפואה בחו״ל Overseas Medical Education בהנהלת ד״ר שחר גבע.
http://www.premedicine.co.il/
";

		private const string premedFemale = @"שלום {שם},

את מוזמנת לפגישת יעוץ במכינה לרפואה בחו״ל בהנהלת ד״ר שחר גבע.
הפגישה תתקיים ביום {יום בשבוע} ה- {תאריך} בשעה {שעה}, במשרדי בית הספר שברחוב יגאל אלון 123, תל אביב.
הכניסה לבניין מרחוב מוזס.
קוד כניסה לבניין 4545#.

מומלץ להגיע לפגישה בליווי ההורים.

בברכה,
ד״ר נעמי טרושינה

יועצת אקדמית,
המכינה לרפואה בחו״ל Overseas Medical Education בהנהלת ד״ר שחר גבע.
http://www.premedicine.co.il/
";

		private const string germanyMale = @"שלום {שם},

אתה מוזמן לפגישת יעוץ עם ד״ר שחר גבע בנושא לימודים אקדמיים בגרמניה.
הפגישה תתקיים ביום {יום בשבוע} ה- {תאריך} בשעה {שעה}, במשרדי בית הספר שברחוב יגאל אלון 123, תל אביב.
הכניסה לבניין מרחוב מוזס.
קוד כניסה לבניין 4545#.

מומלץ להגיע לפגישה בליווי ההורים.

בברכה,
ד״ר נעמי טרושינה

יועצת אקדמית,
,Think Germany
בהנהלת ד״ר שחר גבע.
http://www.thinkgermany.co.il/";

		private const string germanyFemale = @"שלום {שם},

את מוזמנת לפגישת יעוץ עם ד״ר שחר גבע בנושא לימודים אקדמיים בגרמניה.
הפגישה תתקיים ביום {יום בשבוע} ה- {תאריך} בשעה {שעה}, במשרדי בית הספר שברחוב יגאל אלון 123, תל אביב.
הכניסה לבניין מרחוב מוזס.
קוד כניסה לבניין 4545#.

מומלץ להגיע לפגישה בליווי ההורים.

בברכה,
ד״ר נעמי טרושינה

יועצת אקדמית,
,Think Germany
בהנהלת ד״ר שחר גבע.
http://www.thinkgermany.co.il/";

	}
}

