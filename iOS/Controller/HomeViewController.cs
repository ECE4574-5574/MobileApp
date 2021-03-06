﻿
using System;

using Foundation;
using UIKit;
using OpenEars;
using OpenEars.Voices;

namespace HomeAutomationApp.iOS
{
public partial class HomeViewController : UIViewController
{
	#region Fields

	OEEventsObserver observer;
	OEPocketsphinxController pocketSphinxController;
	OEFliteController fliteController;

	Slt slt;

	String pathToLanguageModel;
	String pathToDictionary;
	String firstVoiceToUse;
	String secondVoiceToUse;

	#endregion

	#region Delegate 
	class MyOpenEarsEventsObserverDelegate : OEEventsObserverDelegate
	{
		HomeViewController controller;

		public MyOpenEarsEventsObserverDelegate (HomeViewController ctrl)
		{
			controller = ctrl;
		}

		public override void PocketsphinxDidReceiveHypothesis (NSString hypothesis, NSString recognitionScore, NSString utteranceID)
		{
			controller.UpdateText ("Heard: " + hypothesis);
			controller.Say ("You said: " + hypothesis);
		}

		public override void AudioSessionInterruptionDidBegin ()
		{
			Console.WriteLine ("AudioSession interruption began.");
			controller.UpdateStatus ("AudioSession interruption began");
			controller.StopListening ();

		}

		public override void AudioSessionInterruptionDidEnd ()
		{
			Console.WriteLine ("AudioSession interruption end.");
			controller.UpdateStatus ("AudioSession interruption end.");
			controller.StartListening ();
		}

		public override void AudioInputDidBecomeUnavailable ()
		{
			Console.WriteLine ("The audio input has become unavailable");
			controller.UpdateStatus ("The audio input has become unavailable");
			controller.StopListening ();
		}

		public override void AudioInputDidBecomeAvailable ()
		{
			Console.WriteLine ("The audio input is available.");
			controller.UpdateStatus ("The audio input is available");
			controller.StartListening ();
		}

		public override void AudioRouteDidChangeToRoute (NSString newRoute)
		{
			Console.WriteLine ("Audio route change. The new route is " + newRoute);
			controller.UpdateStatus ("Audio route change. The new route is " + newRoute);
			controller.StopListening ();
			controller.StartListening ();
		}

		public override void PocketsphinxDidStartCalibration ()
		{
			Console.WriteLine ("Pocketsphinx calibration has started.");
			controller.UpdateStatus ("Pocketsphinx calibration has started");
		}

		public override void PocketsphinxDidCompleteCalibration ()
		{
			Console.WriteLine ("Pocket calibration is complete");
			controller.UpdateStatus ("Pocket calibratio is complete");
		}

		public override void PocketsphinxRecognitionLoopDidStart ()
		{
			Console.WriteLine ("Pocketsphinx is starting up");
			controller.UpdateStatus ("Pocketsphinx is starting up");
		}

		public override void PocketsphinxDidStartListening ()
		{
			Console.WriteLine ("Pocketsphinx is now listening");
			controller.UpdateStatus ("Pocketphinx is now listening");
			controller.UpdateButtonStates (true, false, false, true);
		}

		public override void PocketsphinxDidDetectSpeech ()
		{
			Console.WriteLine ("Pocketsphinx has detected speech");
			controller.UpdateStatus ("Pocketsphinx has dedected speech");
		}

		public override void PocketsphinxDidDetectFinishedSpeech ()
		{
			Console.WriteLine ("Pocketphinx has detected a second of silence, concluding utterance.");
			controller.UpdateStatus ("Pocketsphinx has dedected finished speech");
		}

		public override void PocketsphinxDidStopListening ()
		{
			Console.WriteLine ("Pocketsphinx has stopped listening");
			controller.UpdateStatus ("Pocketsphinx has stopped listening");
		}

		public override void PocketsphinxDidSuspendRecognition ()
		{
			Console.WriteLine ("Pocketsphinx has suspended recognition");
			controller.UpdateStatus ("Pocketsphinx has suspended recognition");
		}

		public override void PocketsphinxDidResumeRecognition ()
		{
			Console.WriteLine ("Pocketsphinx has resumed recognition");
			controller.UpdateStatus ("Pocketsphinx has resumed recognition");
		}

		public override void FliteDidStartSpeaking ()
		{
			Console.WriteLine ("Flite has started speaking");
			controller.UpdateStatus ("Flite has started speaking.");
		}

		public override void FliteDidFinishSpeaking ()
		{
			Console.WriteLine ("Flite has finished speaking.");
			controller.UpdateStatus ("Flite has finished speaking");
		}

		public override void PocketSphinxContinuousSetupDidFail ()
		{

		}

	}

	#endregion

	#region Contructor
	public HomeViewController() :
		base("HomeViewController", null)
	{
		observer = new OEEventsObserver ();
		observer.Delegate = new MyOpenEarsEventsObserverDelegate (this);
		pocketSphinxController = new OEPocketsphinxController ();
		fliteController = new OEFliteController ();
		slt = new Slt();

		firstVoiceToUse = "cmu_us_slt";
		secondVoiceToUse = "cmu_us_rms";
		pathToLanguageModel = NSBundle.MainBundle.ResourcePath + System.IO.Path.DirectorySeparatorChar + "OpenEars1.languagemodel";
		pathToDictionary = NSBundle.MainBundle.ResourcePath + System.IO.Path.DirectorySeparatorChar + "num.dic";
	}
	#endregion

	#region ViewController Overrides
	public override void DidReceiveMemoryWarning()
	{
		// Releases the view if it doesn't have a superview.
		base.DidReceiveMemoryWarning();
			
		// Release any cached data, images, etc that aren't in use.
	}

	public override void ViewDidLoad()
	{
		base.ViewDidLoad();
			
		// Perform any additional setup after loading the view, typically from a nib.
	}
	#endregion

	#region Update
	public void UpdateStatus (String text)
	{
//		statusTextView.Text = text;
	}

	public void UpdateText (String text)
	{
//		heardTextView.Text = text;
	}

	public void UpdateButtonStates (bool hidden1, bool hidden2, bool hidden3, bool hidden4)
	{
//		startListeningButton.Hidden = hidden1;
//		stopListeningButton.Hidden = hidden2;
//		suspendRecognitionButton.Hidden = hidden3;
//		resumeRecognitionButton.Hidden = hidden4;
	}

	public void Say (String text)
	{
		fliteController.Say (text, slt);
	}

	public void StartListening ()
	{
//		pocketSphinxController.StartListeningWithLanguageModelAtPathdictionaryAtPathacousticModelAtPathlanguageModelIsJSGF (
//			pathToLanguageModel,
//			pathToDictionary,
//			false
//		);
	}

	public void StopListening ()
	{
		pocketSphinxController.StopListening ();
	}

	public void SuspendRecognition ()
	{
		pocketSphinxController.SuspendRecognition ();
	}

	public void ResumeRecognition ()
	{
		pocketSphinxController.ResumeRecognition ();
	}
	#endregion

}
}

