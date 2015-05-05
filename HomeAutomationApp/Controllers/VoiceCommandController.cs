﻿using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Diagnostics;

namespace HomeAutomationApp
{
public class VoiceCommandController
{
	public VoiceCommandController()
	{
	}

	//handles sending a user coordinate json blob to the decision system.
	public HttpResponseMessage makeItBrighterNearMe()
	{

		JObject blob = new JObject();
		String timeStamp = GetTimestamp(DateTime.Now);

		blob["lat"] = 98.543;
		blob["long"] = 84.345;
		blob["alt"] = 45.3454;
		blob["time"] = timeStamp;

		var result = SendBrighterAsync(blob.ToString());
		return result;
	}

	public static String GetTimestamp(DateTime value)
	{
		return value.ToString("yyyyMMddHHmmssfff");
	}

	public static HttpResponseMessage SendBrighterAsync(string packet)
	{

		var client = new HttpClient();
		client.Timeout = TimeSpan.FromSeconds(10);

		client.BaseAddress = new Uri(ConfigModel.Url);

		try
		{
			var response = client.PostAsync("http://serverapi1.azurewebsites.net/api/app/user/brighten/", 
				               new StringContent(packet, Encoding.UTF8, "application/json")).Result;

			return response;

		}
		catch(Exception e)
		{
			Debug.WriteLine("HomeAutomationDebugError - Position Update Error: " + e.Message);
			Debug.WriteLine("HomeAutomationDebugError - Position Update Error: " + e.InnerException.Message);
		}

		return null;
	}

}
}