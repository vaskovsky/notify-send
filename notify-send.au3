; Copyright © 2015 Alexey Vaskovsky
;
; This program is free software; you can redistribute it and/or
; modify it under the terms of the GNU Lesser General Public
; License as published by the Free Software Foundation; either
; version 3.0 of the License, or (at your option) any later version.
;
; This program is distributed in the hope that it will be useful,
; but WITHOUT ANY WARRANTY; without even the implied warranty of
; MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
; See the GNU Lesser General Public License for more details.
#NoTrayIcon
Opt("TrayMenuMode", 1)
TraySetState()
$null_title = "<title_not_entered>"
$title = $null_title
$message = ""
$icon = 0
$timeout = 10000
For $i = 1 To $cmdline[0]
	If $cmdline[$i] == "-i" Then
		If $i < $cmdline[0] Then
			$i = $i + 1
			If $cmdline[$i] == "info" Then
				$icon = 1
			ElseIf $cmdline[$i] == "important" Then
				$icon = 2
			ElseIf $cmdline[$i] == "error" Then
				$icon = 3
			EndIf
		EndIf
	ElseIf $cmdline[$i] == "-t" Then
		If $i < $cmdline[0] Then
			$i = $i + 1
			If StringIsInt($cmdline[$i]) Then
				$timeout = $cmdline[$i]
			EndIf
		EndIf
	ElseIf $title == $null_title Then
		$title = $cmdline[$i]
	Else
		$message = $cmdline[$i]
	EndIf
Next
If $title == $null_title Then
	$title = "notify-send for Windows"
	$message = "Usage: " & @CRLF & _
		'notify-send [-i ICON] [-t TIMEOUT] "TITLE" "MESSAGE"' & @CRLF & _
		"(c) Alexey Vaskovsky, 2012-2016" & @CRLF & _
		"For more information visit http://vaskovsky.net/notify-send"
	$icon = 1
	$timeout = 35000
EndIf
If $icon == 0 Then
	$icon = 1
EndIf
TrayTip($title, $message, $timeout, $icon)
Sleep($timeout)
