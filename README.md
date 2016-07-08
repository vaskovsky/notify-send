# notify-send for Windows

notify-send is a tool that displays pop-up desktop notifications.

## Building

1. Run [Aut2Exe][1].
2. Choose `notify-send.au3` as `Source (AutoIt *.au3)`.
3. Choose `notify-send.ico` as `Custom Icon (*.ico file)`.
4. Press `Convert`.

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

[1]: https://www.autoitscript.com/site/autoit/downloads/
