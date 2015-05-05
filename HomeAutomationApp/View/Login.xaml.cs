using System;
using System.Collections.Generic;
using Xamarin.Forms;

/*
 * Login page is start page, replace it with yours.
 * But for the navigation to work properly, use Navigation.PushAsync(new nextPage());
 */


namespace HomeAutomationApp
{
public partial class Login : ContentPage
{
	public Login ()
	{
		InitializeComponent ();

		ClickButton.Clicked += (object sender, EventArgs e) => {
			if(UserField.Text == "" || PassField.Text == "")
				Text.Text = "Provide a username and passoword";
			else{
				//Navigation.PushAsync(new MainTabbedView());
				if(LoginController.RequestLogin(UserField.Text, PassField.Text)){
					Text.Text = "Logged In";
					User.setUsername(UserField.Text);
					User.setPassword(PassField.Text);
				}
				else
					Text.Text = "Not Recognized, are you registered?";
			}
		};

		RegisterButton.Clicked += (object sender, EventArgs e) => {
			if(UserField.Text == "" || PassField.Text == "")
				Text.Text = "Provide a username and passoword";
			else{
				if(LoginController.RegisterUser(UserField.Text, PassField.Text)){
					Text.Text = "Registration Completed!, Please Login";
				}
				else
					Text.Text = "Registration Failed";
			}

		};
	}
}
}

