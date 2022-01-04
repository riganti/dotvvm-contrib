# CookieBar

This control renders a cookie bar with an option to customize cookie preferences. The settings are stored in local storage and it relies on Google Tag Manager `window.dataLayer`.

## Sample 1: Basic usage

If you don't have any optional cookies on the site, you can just use the control like this:

```DOTHTML
<dc:CookieBar ... />
```

To override the default texts, you can set any of the control properties:

* `Title`
* `Description`
* `DialogSubtitle`
* `NecessaryCookiesTitle`
* `NecessaryCookiesDescription`
* `MoreOptionsButtonText`
* `AcceptAllButtonText`
* `SaveAndCloseButtonText`
* `AlwaysAllowedText`
* `AllowedText`
* `DisallowedText`

<br />

## Sample 2: Rules

To customize the optional cookies, you can use one of the built-in rules, or define your own:

```DOTHTML
<dc:CookieBar>
	<dc:GoogleAnalyticsRule />
	<dc:GoogleAdsRule />
	<dc:FacebookPixelRule />
	<dc:SmartlookRule />
	<dc:CookieBarRule Key="key_in_google_tag_manager" Title="Some title" Description="Longer description why you need the cookie" />
</dc:CookieBar>
```

<br />

## Sample 3: Reset the consent

If you want to provide user with a button to reset the consent and display the cookie bar again, call the following JavaScript function. Pass `true` if you want to display the cookie bar, or `false` if you just need to clear the information stored in the local storage.

```
<input type="button" 
       onclick="DotVVM.Contrib.CookieBar.resetConsent(true);" 
       value="Reset consent" />
```