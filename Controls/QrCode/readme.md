# QrCode

Controls render canvas with qrcode based od Url property. It only support **Client Side rendering**. 

## Sample 1: Something

You can do this:

```DOTHTML
<dc:QrCode Content="{value: Url}"></dc:QrCode>
<dc:QrCode Content="https://www.google.com/"></dc:QrCode>
```

## Prerequisites

This contrib control requires resource "jquery". You could add this resource by [libman](https://docs.microsoft.com/en-us/aspnet/core/client-side/libman/?view=aspnetcore-2.2).

## Info about used script

Used script is copied from https://github.com/jeromeetienne/jquery-qrcode.
QRErrorCorrectLevel - have default value set to QRErrorCorrectLevel.H => highest possible value
