using System;
using System.Timers;
using System.Reflection;

using TXM.Core;

namespace TXM.Lin
{
	public partial class TimerWindow : Gtk.Window
	{
		private Timer timer;
		private int min, sec, Minute = 75;
		public bool Started { get; private set; }
		private bool whiteText = false;
		public IO Io { get; set; }

		public event EventHandler Changed;

		public TimerWindow (IO io) :
		base (Gtk.WindowType.Toplevel)
		{
			this.Build ();

			Io = io;

			Started = false;

			whiteText = io.GetColor ();

			IntegerUpDownMinutes.Value = Minute;

			timer = new Timer();
			timer.Interval = 1000;
			timer.Elapsed += new ElapsedEventHandler(timer_Tick);

			string imgurl = Io.GetImagePath ();
			if (imgurl != "" && imgurl == null)
			{
				Io.CopyImage();
				SetImage();
			}
			radiobuttonWhite.Active = whiteText;
			radiobuttonBlack.Active = !whiteText;
			ChangeLabelColor();
		}

		private void ButtonStart_Click(object sender, EventArgs e)
		{
			StartTimer();
		}

		public void StartTimer()
		{
			min = Minute;
			sec = 0;
			aktZeit();
			start();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (sec == 0)
			{
				if (min == 0)
					stop();
				else
				{
					min--;
					sec = 59;
				}
			}
			else
				sec--;

			aktZeit ();

			if (Changed != null)
			{
				Changed(this, new EventArgs());
			}
		}

		public string ActualTime
		{
			get
			{
				return LabelTime.Text.ToString();
			}
		}

		private void aktZeit()
		{
			LabelTime.Text = min.ToString("D2") + ":" + sec.ToString("D2");
		}

		private void ButtonPause_Click(object sender, EventArgs e)
		{
			PauseTimer();
		}

		public void PauseTimer()
		{
			if (Started)
				stop();
			else
				start();
		}

		private void ButtonReset_Click(object sender, EventArgs e)
		{
			ResetTimer();
		}

		public void ResetTimer()
		{
			stop();
			min = Minute;
			sec = 0;
			aktZeit();
			if (Changed != null)
			{
				Changed(this, new EventArgs());
			}
		}

		private void stop()
		{
			timer.Stop();
			ButtonPause.Label = "Fortsetzen";
			Started = false;
		}

		private void start()
		{
			timer.Start();
			ButtonPause.Label = "Pausieren";
			Started = true;
		}

		private void SetImage_Click(object sender, EventArgs e)
		{
			Io.NewImage ();
			SetImage();
		}

		private void ChangeLabelColor()
		{
			string temp = LabelTime.Text;
			if (whiteText)
			{
				labelMinutes.Markup = "<span foreground=\"white\">Minuten</span>";
				LabelTime.Markup =  "<span foreground=\"white\" font_desc=\"250.0\">" + temp + "</span>";
			}
			else
			{
				labelMinutes.Markup = "<span foreground=\"black\">Minuten</span>";
				LabelTime.Markup =  "<span foreground=\"black\" font_desc=\"250.0\">" + temp + "</span>";
			}
		}

		protected void SetWhite (object sender, EventArgs e)
		{
			whiteText = true;
			Io.WriteColor (whiteText);
			ChangeLabelColor();
		}

		protected void SetBlack (object sender, EventArgs e)
		{
			whiteText = false;
			Io.WriteColor (whiteText);
			ChangeLabelColor();
		}

		protected void Minutes_Changed (object o, EventArgs args)
		{
			Minute = (int)IntegerUpDownMinutes.Value;
		}

		private void SetImage()
		{
			try {
				//Set Background
			} catch (Exception e) {
				Io.ShowMessage ("Die Bilddatei " + Io.TempImgPath + " ist kein gültiges Bild.");
			}
		}
	}
}

