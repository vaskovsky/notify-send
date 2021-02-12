# notify-send for Windows

notify-send is a tool that displays pop-up desktop notifications.

## Building

1. Open `notify-send.sln` in **Visual Studio** or **MonoDevelop**.

2. Select **Release** from the **Solution Configuration** drop-down list,
   which is on the **Standard** toolbar.

3. On the **Build** menu, click **Build**.

You can find `notify-send.exe` in `bin/Release` folder.

## Usage

	notify-send [OPTIONS] "TITLE" ["MESSAGE"]

### Options:

`-i ICON`:
specifies an icon to display.
The possible values of `ICON` are: `info` | `important` | `error`.
Default: `info`.

`-a APPNAME`:
specifies the application name for the icon.

`-t TIMEOUT`:
specifies the timeout in milliseconds at which to expire the notification.
Default: 5000.

This parameter is deprecated as of Windows Vista.
Notification display times are now based on system accessibility settings.

`-v`:	show version and exit.

`-?`:	show help options and exit.

`--debug`:	enables the debug mode.

`-u LEVEL`, `-c TYPE`, `-h HINT` are ignored
(designed for compatibility with the Linux version).

`--`:	end of options.

### Examples:

	notify-send "My Message"

	notify-send "Title" "Message"

	notify-send -i error "Error" "File not found"

	notify-send -i important "Attention!" "You should upgrade some software"

## License

Copyright © 2012-2016 Alexey Vaskovsky <alexey@vaskovsky.net>

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the [GNU Lesser General Public License][1] for more details.

[1]: LICENSE.txt
