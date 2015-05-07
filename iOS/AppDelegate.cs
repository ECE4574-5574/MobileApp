using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace HomeAutomationApp.iOS
{
[Register("AppDelegate")]
public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
{
	public override bool FinishedLaunching(UIApplication app, NSDictionary options)
	{
		global::Xamarin.Forms.Forms.Init();

		LoadApplication(new App());

		if(UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
		{
			var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Sound |
			               UIUserNotificationType.Alert | UIUserNotificationType.Badge, null);

			UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
		}
		else
		{
			
		}

		return base.FinishedLaunching(app, options);
	}

	public override void DidRegisterUserNotificationSettings(UIApplication application,
	                                                         UIUserNotificationSettings notificationSettings)
	{ 
		application.RegisterForRemoteNotifications(); 
	}

	public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
	{
		var token = deviceToken.ToString();
		Console.WriteLine("Device Token " + token); 
		// code to register with your server application goes here
	}

	public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
	{
		new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
	}

	public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
	{
		Console.WriteLine(userInfo.ToString());

		NSObject inAppMessage = userInfo.ValueForKeyPath(new NSString("aps.alert"));

		var alert = new UIAlertView("Home Automation", inAppMessage.ToString(), null, "OK", null);
		alert.Show();

//		InvalidationController.invalidate();

	}

}
}

