# InstallBar

This control renders a bar which sticks to the bottom of the screen and hints the user to install the web app (PWA).

* In browsers supporting the native install prompt (Chrome, Edge, and other Chromium-based browsers), the bar offers an **Install** button which opens the browser's install dialog. The default browser install mini-infobar is suppressed.
* On iOS / iPadOS, where the app has to be added to the home screen manually, the bar displays instructions how to do it.
* In other browsers, the bar is not displayed automatically.
* The bar is never displayed when the app already runs in the standalone mode (installed app).
* The user can close the bar. The decision is remembered in the session storage, so the bar will not appear again until the browser session ends.

The page must link a [web app manifest](https://developer.mozilla.org/en-US/docs/Web/Manifest) so the browser considers the app installable.

## Sample 1: Basic usage

Place the control in the master page so it is available on all pages:

```DOTHTML
<dc:InstallBar IconUrl="/icon-192.png" />
```

To override the default texts (English and Czech localizations are included), you can set any of the control properties:

* `Title`
* `Description` - displayed when the browser offers the native install prompt
* `IosInstructions` - displayed on iOS / iPadOS
* `ManualInstructions` - displayed when the bar is shown explicitly in a browser without the native install prompt
* `InstallButtonText`
* `CloseButtonText` - tooltip and accessible label of the close button

Other properties:

* `IconUrl` - URL of an icon displayed in the bar (typically the app icon)
* `StorageKey` - session storage key used to remember that the user has closed the bar (default `dotvvm-contrib-install-bar-dismissed`)

<br />

## Sample 2: Show the bar again from a menu

The control registers a global JavaScript API which can be used e.g. from a menu item. The `show` function displays the bar even if the user closed it before. It does nothing and returns `false` if the app is running in the standalone mode.

```DOTHTML
<a href="#" onclick="DotVVM.Contrib.InstallBar.show(); return false;">Install app</a>
```

The API offers these functions:

* `DotVVM.Contrib.InstallBar.show()` - shows the bar and clears the remembered dismissal; returns `false` in the standalone mode
* `DotVVM.Contrib.InstallBar.hide()` - hides the bar without remembering the decision
* `DotVVM.Contrib.InstallBar.install()` - opens the native install prompt if available (otherwise shows the bar with instructions); returns a promise resolving to `true` if the user accepted the installation
* `DotVVM.Contrib.InstallBar.isStandalone()` - returns `true` if the app runs as an installed app - useful to hide the menu item
* `DotVVM.Contrib.InstallBar.canPrompt()` - returns `true` if the browser currently offers the native install prompt

<br />

## Styling

The bar is styled using the `dotvvm-contrib-install-bar` CSS classes and can be customized using these CSS variables:

```CSS
.dotvvm-contrib-install-bar {
    --dotvvm-contrib-install-bar-background: #1f2937;
    --dotvvm-contrib-install-bar-color: #f9fafb;
    --dotvvm-contrib-install-bar-accent: #2563eb;
    --dotvvm-contrib-install-bar-accent-color: #ffffff;
}
```

When the bar is visible, the `<html>` element gets the `dotvvm-contrib-install-bar-visible` class and the `--dotvvm-contrib-install-bar-height` CSS variable, so you can make room for the bar at the end of the page:

```CSS
html.dotvvm-contrib-install-bar-visible body {
    padding-bottom: var(--dotvvm-contrib-install-bar-height);
}
```
