# CookieBar

This control renders a cookie bar with an option to customize cookie preferences. The settings are stored in local storage and it relies on Google Tag Manager `window.dataLayer`.

## Google consent initialization

The control inserts a consent default script at the beginning of the page's `head`, before GTM.
New visitors default to denied for exactly the rules configured in the component.
Returning visitors' saved choices for configured rules are restored
synchronously before GTM starts. Only the exact stored value `granted`, together with a saved consent
marker, grants a rule. If storage cannot be read, defaults remain denied and a warning is logged.
Unconfigured consent types are omitted, not implicitly denied. Configure all consent types needed by
your tags; old stored choices for rules no longer present are ignored.
The dialog reflects saved choices; optional choices are not preselected for new visitors.
If any configured rule has no valid saved choice (`granted` or `denied`), the banner appears again.
Existing valid choices are preserved, and new or invalid choices remain denied until saved.
Saving either grants or rejections records all current rules, so a saved rejection does not reprompt.

For advanced consent mode, load GTM on every page and configure the Google Analytics base/destination
tag to fire on Initialization - All Pages with No additional consent required. Its built-in consent
checks adapt collection to the current state. Do not also install a second base tag.
All Pages / Page View can also work, provided consent defaults run first.
The control does not itself load GTM, GA4, or third-party analytics.
Denied consent does not guarantee that cookieless measurements will appear as ordinary GA4 sessions.
Requests before consent are expected in advanced mode; they do not mean consent was granted.
On a fresh visit or after rejecting all purposes, all configured consent types should be denied.
To explicitly deny all four Google types, include all four built-in Google rules shown below.
Accepting analytics alone then grants only `analytics_storage`, leaving advertising purposes denied.
Ensure no other consent script or GTM tag overrides these states.

Consent keys are case-sensitive: `analytics_storage` is valid; `analytics_Storage` is a different key.
Custom third-party rules require a corresponding GTM/vendor integration.
`GoogleAdsRule` controls `ad_storage` only. For Consent Mode v2, explicitly configure
`GoogleAdsUserDataRule` and `GoogleAdsPersonalizationRule` according to your policy; the library does not infer
permission for those purposes from an older storage-only choice:

```DOTHTML
<dc:GoogleAnalyticsRule />
<dc:GoogleAdsRule />
<dc:GoogleAdsUserDataRule />
<dc:GoogleAdsPersonalizationRule />
```

The two additional advertising rules supply their own keys, titles and descriptions.
They do not delete cookies themselves: data sharing and personalization are consent signals,
whereas `GoogleAdsRule` handles advertising-storage cookie cleanup.
Review and override the default descriptions as needed to accurately disclose your site's processing.

### Consent purposes and rule keys

`Key` is one consent identifier, not a space-separated list. Keep separate choices for distinct
purposes, such as analytics, advertising measurement/data sharing, and personalized advertising.
Consent Mode parameters are technical signals, not a legal taxonomy: applicable law and the actual
purposes disclosed by your site determine the required granularity. The control does not certify
legal compliance. In particular, `GoogleAdsRule` has not been expanded to grant new purposes from
previously saved `ad_storage` consent. Use the explicit rules above when you need those purposes.
See the [EDPB consent guidelines](https://www.edpb.europa.eu/documents/guideline/guidelines-052020-on-consent-under-regulation-2016679_en).

### Microsoft Clarity

Use `<dc:ClarityRule />` for a separate session-analysis choice. Its custom Google consent-queue key
is `clarity_storage`; it matches Clarity's first-party `_clck` and `_clsk` cookies.
This rule does not load Clarity or automatically call its API. Your GTM/vendor integration must
read this consent, handle subsequent changes, and pass the corresponding state to Clarity:

```javascript
window.clarity('consentv2', {
    analytics_Storage: clarityAnalyticsConsent,
    ad_Storage: clarityAdvertisingConsent
});
```

Both variables must be explicit `"granted"` or `"denied"` values. Map `clarity_storage` to
`clarityAnalyticsConsent`; leave `clarityAdvertisingConsent` denied unless you separately obtain
consent for Microsoft's advertising purposes. Google advertising consent must not implicitly
grant Microsoft advertising consent. Set the initial states before Clarity records, restore
saved choices on each page, and forward rejections as well as grants. Merely requiring additional
consent on a GTM tag does not implement revocation or this API forwarding.

`analytics_Storage` and `ad_Storage` are the exact, case-sensitive **Clarity API parameter names**,
not Google consent types. An old custom rule using `analytics_Storage` can be migrated to
`ClarityRule`, but its saved grant is not copied to `clarity_storage`; obtain a new choice.
See [Clarity Consent API v2](https://learn.microsoft.com/en-us/clarity/setup-and-installation/clarity-consent-api-v2).

### Cookie cleanup scope

Built-in patterns cover default first-party cookie names:

| Rule | Cookies |
| --- | --- |
| Google Analytics | `_ga`, `_ga_*`, `_gid`, `_gat`/`_gat_*`, `_gac_*`, legacy `__utm` cookies, `_dc_gtm_*`, `AMP_TOKEN`, `FPID`, `FPLC` |
| Google Ads | `_gcl_*`, `_gac_*`, `FPGCLAW`, `FPGCLGB`, `FPGCLDC`, `FPGCLGS`, `FPAU`, `FPGSID` |
| Facebook Pixel | `_fbp`, `_fbc` |
| Smartlook | Legacy `SL` and `SL_C_<project>_SID`/`VID`/`KEY`/`DOMAIN` |
| Microsoft Clarity | `_clck`, `_clsk` |

`_gac_*` is intentionally shared by Analytics and Ads rules; rejecting either removes it.
Override `CookieNameRegexes` for custom cookie prefixes or additional cookies used by your deployment.
Cleanup expires JavaScript-visible cookies at path `/` for the current host and candidate parent
domains, plus host-only cookies. It cannot remove HttpOnly cookies, cookies on vendor domains
(such as Clarity's `CLID`, `ANONCHK`, `MR`, `MUID`, `SM`, or Google's `IDE`), cookies at other paths,
or server-side data. Server-set cookies such as `FPID` may be HttpOnly and need server-side cleanup.
Smartlook's `SL_L_*` and `SMARTLOOK_LS_QUEUED_CHUNKS` are local-storage data, not cookies.
Cookie deletion alone does not stop a running vendor script from collecting data or recreating
cookies; implement the vendor's consent/revocation mechanism as well.

Cookie references:
[Google](https://business.safety.google/adscookies/),
[Meta](https://developers.facebook.com/docs/marketing-api/conversions-api/parameters/fbp-and-fbc/),
[Smartlook](https://help.smartlook.com/docs/cookies-smartlook),
[Clarity](https://learn.microsoft.com/en-us/clarity/setup-and-installation/clarity-cookies).

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
	<dc:GoogleAdsUserDataRule />
	<dc:GoogleAdsPersonalizationRule />
	<dc:FacebookPixelRule />
	<dc:SmartlookRule />
	<dc:ClarityRule />
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

<br />

## Sample 4: Change cookie settings

If you want to let the user review or change their choices without resetting them, call the following JavaScript function.
It opens the cookie settings dialog with the currently saved choices preselected. Nothing is changed until the user clicks the save button;
closing the dialog (e.g. by pressing Esc) keeps the current consent.

```
<input type="button" 
       onclick="DotVVM.Contrib.CookieBar.showSettings();" 
       value="Cookie settings" />
```