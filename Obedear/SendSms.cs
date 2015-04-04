using System;
using System.Collections.Generic;

namespace Obedear
{
	public interface ISendSms 
	{
		void Send(string phone, string sms);

		List<string> GetCalls();
	}
}

