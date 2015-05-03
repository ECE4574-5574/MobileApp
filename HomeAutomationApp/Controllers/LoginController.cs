using System;
using System.Net.Http;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HomeAutomationApp
{
public static class LoginController
{
	//private bool registered = false;
	public static bool RequestLogin(string username, string password)
	{
//		if(getUser(username, password).Result.StatusCode == System.Net.HttpStatusCode.OK)
//			return true;
//		else
//			return false;
		return true;
	}

	public static bool RegisterUser(string username, string password)
	{
		JObject blob = new JObject();
		blob["username"] = username;
		//blob["password"] = User.GetHash(password);
		blob["password"] = password;
		if(SendUserAsync(blob.ToString()).IsSuccessStatusCode)
			return true;
		else
			return false;
	}

	public static HttpResponseMessage SendUserAsync(string packet)
	{
		try
		{
			var client = new HttpClient();
			client.Timeout = TimeSpan.FromSeconds(10);


			var response = client.PostAsync("http://serverapi1.azurewebsites.net/api/storage/user/", 
				new StringContent(packet, Encoding.UTF8, "application/json")).Result;

			var status = response.StatusCode;

			return response;
		}

		catch(Exception e)
		{
			Debug.WriteLine("HAD - Position Update Error: " + e.Message);
			Debug.WriteLine("HAD - Position Update Error: " + e.InnerException.Message);
		}

		return null;
	}

	public static async Task<HttpResponseMessage> getUser(string username, string password)
	{
		var client = new HttpClient();
		client.Timeout = TimeSpan.FromSeconds(10);
		var response = await client.GetAsync("http://serverapi1.azurewebsites.net/api/app/user/userid/" + username + "/" + password);
		var status = response.StatusCode;
		return response;
	}
}
}

