﻿/**
 * © 2016-2021 Alexey Vaskovsky <alexey@vaskovsky.net>
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
using System;
using System.Windows.Forms;
using System.Reflection;
namespace Vaskovsky
{
	/** notify-send is a tool that displays pop-up desktop notifications. */
	public class NotifySendCLI : ApplicationContext
	{
		private const string Version = "notify-send for Windows 4.0.0";
		private const int DefaultTimeout = 10000;
		private const string DefaultAppName = "notify-send";
		private const string DefaultTitle = DefaultAppName;
		private const string Usage = @"Usage:
notify-send [-i info|important|error] ""TITLE"" ""MESSAGE""

© 2016-2021 Alexey Vaskovsky
For more information visit
http://vaskovsky.net/notify-send
";
		private NotifyIcon Notification;		
		private NotifySendCLI()
		{
			Notification = new NotifyIcon();
			Notification.Icon = System.Drawing.Icon.ExtractAssociatedIcon(
					Assembly.GetExecutingAssembly().Location);
			Notification.Text = DefaultAppName;
			Notification.BalloonTipIcon = ToolTipIcon.Info;
			Notification.BalloonTipTitle = DefaultTitle;
			Notification.BalloonTipClosed += new EventHandler(this.ExitApplication);
			Notification.BalloonTipClicked += new EventHandler(this.ExitApplication);
			Notification.Click += new EventHandler(this.ExitApplication);
		}
		private void ExitApplication(object sender, EventArgs e)
		{
			Notification.Visible = false;
			Application.Exit();
		}
		/** Parses arguments and runs this application. */
		static void Main(string[] args)
		{
			try
			{
				var notifyCLI = new NotifySendCLI();
				var notify = notifyCLI.Notification;
				var message = "";
				var warning = "";
				int timeout = 0;
				bool hasTitle = false;
				bool optionsEnd = false;
				for (int i = 0; i < args.Length; i++)
				{
					if (!optionsEnd)
					{
						if (args[i] == "-i")
						{
							i++;
							if (i < args.Length)
							{
								if (args[i] == "info")
								{
									notify.BalloonTipIcon = ToolTipIcon.Info;
								}
								else if (args[i] == "important")
								{
									notify.BalloonTipIcon = ToolTipIcon.Warning;
								}
								else if (args[i] == "error")
								{
									notify.BalloonTipIcon = ToolTipIcon.Error;
								}
							}
							continue;
						}
						if (args[i] == "-t")
						{
							i++;
							if (i < args.Length)
							{
								try
								{
									timeout = Int32.Parse(args[i]);
								}
								catch (Exception error)
								{
									warning = "notify-send: timeout: " +
										error.Message + " [" + error.GetType() + "]";
								}
							}
							continue;
						}
						if (args[i] == "-a")
						{
							i++;
							if (i < args.Length)
							{
								notify.Text = args[i];
							}
							continue;
						}
						if (args[i] == "-u" || args[i] == "-c" || args[i] == "-h")
						{
							i++;
							continue;
						}
						if (args[i] == "--debug")
						{
							continue;
						}
						if (args[i] == "-v")
						{
							Console.WriteLine(Version);
							return;
						}
						if (args[i] == "-?")
						{
							Console.WriteLine(Usage);
							return;
						}
						if (args[i] == "--")
						{
							optionsEnd = true;
							continue;
						}
					}
					if (!hasTitle)
					{
						notify.BalloonTipTitle = args[i];
						hasTitle = true;
						continue;
					}
					if (message != "")
					{
						message += " ";
					}
					message += args[i];
				}
				if (message == "" && !hasTitle)
				{
					notify.BalloonTipTitle = DefaultTitle;
					message = Usage;
				}
				if (warning != "")
					Console.Error.WriteLine(warning);
				notify.BalloonTipText = message;
				notify.Visible = true;
				notify.ShowBalloonTip(timeout);
				Application.Run(notifyCLI);
			}
			catch (Exception error)
			{
				Console.Error.WriteLine("notify-send: " + error.Message +
					" [" + error.GetType() + "]");
			}
		}
	}
}
