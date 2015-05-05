﻿using System;
using Gcm.Client;
using System.Net;
using Android.Util;
using System.Net.Http;
using Android.Content;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

//this is for notification
//POST  api/app/user/notify/{username}/{devicetoken}
//
//and this one is to update/change notification
//
//POST api/app/user/devicetoken/{username}/{pass}
using System.Text;

namespace HomeAutomationApp.Droid
{
public static class GCMModel
{
	private static string deviceID;
	private static Context c;
	private static bool registered;
	const string TAG = "GCM-Client";
	public static void Init(Context context)
	{
		c = context;
		GcmClient.CheckDevice(c);
		GcmClient.CheckManifest(c);
		registered = GcmClient.IsRegistered(c);
		if(registered)
			deviceID = GcmClient.GetRegistrationId(c);
		else
			register();
	}

	public static string getDeviceID()
	{
		return deviceID;
	}

	public static void setDeviceID(string id)
	{
		deviceID = id;
	}

	public static void register()
	{
		GcmClient.Register(c, MyGCMBroadcastReceiver.SENDER_IDS);	
	}

	public static void unregister()
	{
		GcmClient.UnRegister(c);
	}

	public static bool isRegistered()
	{
		return registered;
	}

	public static HttpResponseMessage SendTokenAsync(string token)
	{
		try
		{
			var client = new HttpClient();
			client.Timeout = TimeSpan.FromSeconds(10);

			JObject blob = new JObject();
			blob["deviceToken"] = token;

			var packet = blob.ToString();
			var username = User.getUsername();
			var password = User.getPassword();
			Log.Info(TAG, password);

			var response = client.PostAsync("http://serverapi1.azurewebsites.net/api/app/user/devicetoken/" + User.getUsername() + "/" + User.getPassword(), 
				new StringContent(packet, Encoding.UTF8, "application/json")).Result;

			return response;
		}

		catch(Exception e)
		{
			Log.Info(TAG, "Token Update Error: " + e.Message);
			Log.Info(TAG, "Token Update Error: " + e.InnerException.Message);
		}

		return null;
	}

	public static HttpResponseMessage SendNotifyAsync(string message)
	{
		try
		{
			var client = new HttpClient();
			client.Timeout = TimeSpan.FromSeconds(10);

			JObject blob = new JObject();
			blob["message"] = message;
			blob["deviceToken"] = GCMModel.getDeviceID();

			string packet = blob.ToString();
			var response = client.PostAsync("http://serverapi1.azurewebsites.net/api/app/user/notify/" + User.getUsername() + "/" + User.getPassword(), 
				new StringContent(packet, Encoding.UTF8, "application/json")).Result;

			return response;
		}

		catch(Exception e)
		{
			Log.Info(TAG, "Token Update Error: " + e.Message);
			Log.Info(TAG, "Token Update Error: " + e.InnerException.Message);
		}

		return null;
	}

}
}

