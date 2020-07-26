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

using System;
using System.Windows.Forms;
using System.Reflection;

namespace Vaskovsky
{
    /** NotifySend notify-send is a tool that displays pop-up desktop notifications. */
    class NotifySend : ApplicationContext
    {
        private const string Version = "notify-send for Windows 2.1.0";
		private const int DefaultTimeout = 5000;
        private const string DefaultAppName = "notify-send for Windows";
        private const string DefaultTitle = DefaultAppName;
        private const string Usage = @"Usage:
  notify-send [-i info|important|error] ""TITLE"" ""MESSAGE""
© 2016 Alexey Vaskovsky
For more information visit http://vaskovsky.net/notify-send
";
        private NotifyIcon Notification;
        private bool _ExitOnClose = false;
        private bool IsRunning = true;

        /** Specifies the debug mode. */
        public bool Debug = false;

        /**
		 * Specifies the timeout in milliseconds
		 * at which to expire the notification.
		 */
        public int Timeout = DefaultTimeout;

        /** Specifies the message title. */
        public string Title
        {
            get { return Notification.BalloonTipTitle; }
            set { Notification.BalloonTipTitle = value; }
        }

        /** Specifies the message text. */
        public string Message
        {
            get { return Notification.BalloonTipText; }
            set { Notification.BalloonTipText = value; }
        }

        /** Specifies the application name for the icon. */
        public string AppName
        {
            get { return Notification.Text; }
            set { Notification.Text = value; }
        }

        /** Specifies an icon to display. */
        public ToolTipIcon Icon
        {
            get { return Notification.BalloonTipIcon; }
            set { Notification.BalloonTipIcon = value; }
        }

        /**
		 * If this property is set to true,
		 * it will exit thread when you close the notification.
		 *
		 * Default: false.
		 */
        public bool ExitOnClose
        {
            get { return _ExitOnClose; }
            set
            {
                if (value && !_ExitOnClose)
                {
                    Notification.BalloonTipClosed += this.Exit;
                    Notification.BalloonTipClicked += this.Exit;
                    Notification.Click += this.Exit;
                }
                if (!value && _ExitOnClose)
                {
                    Notification.BalloonTipClosed -= this.Exit;
                    Notification.BalloonTipClicked -= this.Exit;
                    Notification.Click -= this.Exit;
                }
                _ExitOnClose = value;
            }
        }

        /** Creates new notification. */
        public NotifySend()
        {
            Notification = new NotifyIcon();
            Notification.Icon = System.Drawing.Icon.ExtractAssociatedIcon(
                Assembly.GetExecutingAssembly().Location);
            AppName = DefaultAppName;
            Title = DefaultTitle;
            Message = "";
            Icon = ToolTipIcon.Info;
            Notification.BalloonTipClosed += new EventHandler(this.Hide);
            Notification.BalloonTipClicked += new EventHandler(this.Hide);
            Notification.Click += new EventHandler(this.Hide);
        }

        /** Creates new notification and sets ExitOnClose = true. */
        [Obsolete("Use NotifySend() and properties.")]
        public NotifySend(
            string title, string message, ToolTipIcon icon, int timeout
        ) : this()
        {
            Title = title;
            Message = message;
            Icon = icon;
            Timeout = timeout;
            ExitOnClose = true;
        }

        /** Displays this notification. */
        public void Show()
        {
            if (Debug)
                Console.WriteLine(Timestamp() + " NotifySend.Show");
            Notification.Visible = true;
            Notification.ShowBalloonTip(Timeout);
        }

        private void Hide(object sender, EventArgs e)
        {
            if (Notification.Visible)
            {
                if (Debug)
                    Console.WriteLine(Timestamp() + " NotifySend.Hide");
                Notification.Visible = false;
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                IsRunning = false;
                if (Debug)
                    Console.WriteLine(Timestamp() + " NotifySend.Exit");
                this.ExitThread();
            }
        }

        private static string Timestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        /** Parses arguments and runs this application. */
        static void Main(string[] args)
        {
            try
            {
                var notifySend = new NotifySend();
                notifySend.ExitOnClose = true;
                var message = "";
                var warning = "";
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
                                    notifySend.Icon = ToolTipIcon.Info;
                                }
                                else if (args[i] == "important")
                                {
                                    notifySend.Icon = ToolTipIcon.Warning;
                                }
                                else if (args[i] == "error")
                                {
                                    notifySend.Icon = ToolTipIcon.Error;
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
                                    notifySend.Timeout = Int32.Parse(args[i]);
                                }
                                catch (Exception error)
                                {
                                    warning = "NotifiSend.Timeout: " +
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
                                notifySend.AppName = args[i];
                            }
                            continue;
                        }
                        if (args[i] == "--debug")
                        {
                            notifySend.Debug = true;
                            continue;
                        }
                        if (args[i] == "-u" || args[i] == "-c" || args[i] == "-h")
                        {
                            i++;
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
                        notifySend.Title = args[i];
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
                    notifySend.Title = DefaultTitle;
                    notifySend.Message = Usage;
                }
                else
                {
                    notifySend.Message = message;
                }
                if (warning != "")
                    Console.Error.WriteLine(warning);
                if (notifySend.Debug)
                {
                    Console.WriteLine(Timestamp() + " NotifySend.Init");
                    Console.WriteLine("\tAppName: " + notifySend.AppName);
                    Console.WriteLine("\tTitle: " + notifySend.Title);
                    Console.WriteLine("\tMessage: " + notifySend.Message);
                    Console.WriteLine("\tIcon: " + notifySend.Icon);
                    Console.WriteLine("\tTimeout: " + notifySend.Timeout);
                }
                notifySend.Show();
                Application.Run(notifySend);
            }
            catch (Exception error)
            {
                Console.Error.WriteLine("NotifySend: " + error.Message +
                    " [" + error.GetType() + "]");
            }
        }
    }
}
