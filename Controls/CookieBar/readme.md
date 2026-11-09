# CookieBar

This control renders a cookie bar with an option to customize cookie preferences. The settings are stored in local storage and it relies on Google Tag Manager `window.dataLayer`.

## Google consent initialization

The control inserts a consent default script at the beginning of the page's `head`, before GTM.
New visitors default to denied for every configured rule. Returning visitors' saved choices are restored
synchronously before GTM starts. Only the exact stored value `granted`, together with a saved consent
marker, grants a rule. If storage cannot be read, defaults remain denied and a warning is logged.
The dialog reflects saved choices; optional choices are not preselected for new visitors.

For advanced consent mode, load GTM on every page and configure the Google Analytics base/destination
tag to fire on Initialization - All Pages with No additional consent required. Its built-in consent
checks adapt collection to the current state. Do not also install a second base tag.
All Pages / Page View can also work, provided consent defaults run first.
The control does not itself load GTM, GA4, or third-party analytics.
Denied consent does not guarantee that cookieless measurements will appear as ordinary GA4 sessions.

Consent keys are case-sensitive: `analytics_storage` is valid; `analytics_Storage` is a different key.
Custom third-party rules require a corresponding GTM/vendor integration.
`GoogleAdsRule` controls `ad_storage` only. For Consent Mode v2, explicitly configure
`ad_user_data` and `ad_personalization` rules according to your policy; the library does not infer
permission for those purposes from an older storage-only choice:

```DOTHTML
<dc:CookieBarRule Key="ad_user_data" Title="Advertising user data" Description="..." />
<dc:CookieBarRule Key="ad_personalization" Title="Personalized advertising" Description="..." />
```

Before deployment, test fresh, accepted, rejected, and returning visits in GTM Preview.
Check initial consent, same-page updates after a choice, and GA4 `page_view` requests.
Use advanced consent mode only where consistent with your consent policy and applicable requirements.

Run the dependency-free JavaScript regression tests with:

```powershell
node --test .\src\DotVVM.Contrib.CookieBar.Tests\consent.test.cjs
```

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