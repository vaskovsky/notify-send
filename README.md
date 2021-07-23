# notify-send for Windows

notify-send is a program to send desktop notifications.

## Building

To compile execute:
```
csc -o+ -sdk:4 -r:System.Windows.Forms -r:System.Drawing -win32icon:notify-send.ico -out:notify-send.exe NotifySend.cs
```

## Documentation

[Usage](http://vaskovsky.net/notify-send.html)

[Troubleshooting](http://vaskovsky.net/notify-send-troubleshooting.html)

[Changelog](http://vaskovsky.net/notify-send-changelog.html)

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
