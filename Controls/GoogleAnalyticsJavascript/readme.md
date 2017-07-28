# GoogleAnalyticsJavascript

Creates JavaScript tracking snippet for Google Analytics client tracking.
For more information visit [Google documentation](https://developers.google.com/analytics/devguides/collection/analyticsjs/#alternative_async_tracking_snippet).

## Sample 1: Hardcoding

You can do this by writing:

```DOTHTML
<dc:GoogleAnalyticsJavascript TrackingId="UA-XXXXX-Y" PageViewEnabled="true" AsyncVersionEnabled="true"/>
```

That will be rendered in HTML and look like this:
```HTML
<script>
    window.ga=window.ga||function(){(ga.q=ga.q||[]).push(arguments)};ga.l=+new Date;
    ga('create', 'UA-XXXXX-Y', 'auto');
    ga('send', 'pageview');
</script>
<script src="https://www.google-analytics.com/analytics.js" async=""></script>
```

<br />

## Sample 2: Dependency Injection

You can also inject `GoogleAnalyticsOptions` via dependency injection. 
1. First you need to register option from configuration:
```CSHARP
services.Configure<GoogleAnalyticsOptions>(
    Configuration.GetSection("GoogleAnalyticsOptions"));
```

2. Then add Google Analytics options to your configuration (e.g.: appsettings.json):
```JSON
{
  "GoogleAnalyticsOptions": {
    "TrackingId": "UA-XXXXX-Z",
    "PageViewEnabled": false,
    "AsyncVersionEnabled": false
  }
}
```

3. As the last step, you need add `dc:GoogleAnalyticsJavascript` in your page:
```DOTHTML
<dc:GoogleAnalyticsJavascript />
```

And this is the result that has been rendered:
```HTML
<script type="text/javascript">
    (function(i, s, o, g, r, a, m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){(i[r].q = i[r].q ||[]).push(arguments)},i[r].l=1*new Date(); a=s.createElement(o),m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a, m)})(window, document,'script','https://www.google-analytics.com/analytics.js','ga');

    ga('create', 'UA-XXXXX-Z', 'auto');       
</script>
```


