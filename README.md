# notify-send for Windows

notify-send is a tool that displays pop-up desktop notifications.

## Building

1. Open `notify-send.sln` in **Visual Studio** or **MonoDevelop**.

2. Select **Release** from the **Solution Configuration** drop-down list,
   which is on the **Standard** toolbar
   (ignore this step in **Visual Studio Express**).

3. On the **Build** menu, click **Build**.

You can find `notify-send.exe` in `bin/Release` folder
(or in `bin/Debug` if you are using **Visual Studio Express**).

## Usage

		notify-send [-i ICON] [-t TIMEOUT] "TITLE" ["MESSAGE"]

### Options

* `ICON`: specifies an icon to display. The possible values are:
`info` | `important` | `error`. Default: info.

* `TIMEOUT`: specifies the timeout in milliseconds at which to
expire the notification. Default: 10000

* `TITLE`: message title.

* `MESSAGE`: message text.

### Examples

		notify-send "My Message"

		notify-send "Title" "Message"

		notify-send -i error "Error" "File not found"

		notify-send -i important "Attention!" "You should upgrade your software"

## License LGPL v3

Copyright © 2015 Alexey Vaskovsky

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3.0 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Lesser General Public License for more details.

## See also

[notify-send Documentation](http://vaskovsky.net/notify-send/)

[Download Visual Studio Express](https://www.visualstudio.com/ru-ru/products/visual-studio-express-vs.aspx)

[Download MonoDevelop](http://www.monodevelop.com/download/)
