using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

/**
 * © 2016 Alexey Vaskovsky <alexey@vaskovsky.net>
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace Vaskovsky
{
	/** NotifySend notify-send is a tool that displays pop-up desktop notifications. */
	class NotifySend : ApplicationContext
	{
		private const int DefaultTimeout = 10000;
		private const string IconFile = "notify-send.ico";
		private const string Title = "notify-send for Windows";
		private const string Usage = @"Usage: 
  notify-send [-i ICON] [-t TIMEOUT] ""TITLE"" ""MESSAGE"" 
© 2016 Alexey Vaskovsky 
For more information visit http://vaskovsky.net/notify-send
";
		private NotifyIcon Notification;
		private int Timeout;

		/** Creates new notification.
         * @param timeout is a timeout in milliseconds.
         */
		public NotifySend (string title, string text, ToolTipIcon icon, int timeout)
		{
			Timeout = timeout;

			Notification = new NotifyIcon ();
			Notification.Icon = Icon.ExtractAssociatedIcon (Assembly.GetExecutingAssembly ().Location); 
			Notification.Text = Title;

			Notification.BalloonTipIcon = icon;
			Notification.BalloonTipTitle = title;
			Notification.BalloonTipText = text;

			Notification.BalloonTipClosed += new EventHandler (this.Exit);
			Notification.BalloonTipClicked += new EventHandler (this.Exit);
			Notification.Click += new EventHandler (this.Exit);
		}

		/** Displays this notification. */
		public void Show ()
		{
			Notification.Visible = true;
			Notification.ShowBalloonTip (Timeout);
		}

		private void Show (object sender, EventArgs e)
		{
			this.Show ();
		}

		private void Exit (object sender, EventArgs e)
		{
			Notification.Visible = false;
			this.ExitThread ();
		}

		/** Parses arguments and runs this application. */
		static void Main (string[] args)
		{
			string title = null;
			var text = "";
			var icon = ToolTipIcon.None;
			int timeout = DefaultTimeout;

			for (int i = 0; i < args.Length; i++) {
				if (args [i] == "-i") {
					i++;
					if (i < args.Length) {
						if (args [i] == "info") {
							icon = ToolTipIcon.Info;
						} else if (args [i] == "important") {
							icon = ToolTipIcon.Warning;
						} else if (args [i] == "error") {
							icon = ToolTipIcon.Error;
						}
					}
				} else if (args [i] == "-t") {
					i++;
					if (i < args.Length) {
						try {
							timeout = Int32.Parse (args [i]);
						} catch (Exception) {
						}
					}
				} else if (title == null) {
					title = args [i];
				} else {
					if (text != "") {
						text += " ";
					}
					text += args [i];
				}
			}

			if (text == "") {
				title = Title;
				text = Usage;
			}

			if (icon == ToolTipIcon.None) {
				icon = ToolTipIcon.Info;
			}

			/* Testing
			Console.WriteLine ("Icon: " + icon);
			Console.WriteLine ("Timeout: " + timeout);
			Console.WriteLine ("Title: " + title);
			Console.WriteLine ("Message: " + text);/**/

			var notifySend = new NotifySend (title, text, icon, timeout);
			notifySend.Show ();
			Application.Run (notifySend);
		}
	}
}
