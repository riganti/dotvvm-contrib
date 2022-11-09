# HeroIcon

Wrapper for SVG [Heroicons](https://heroicons.com/) v 2.0.12

## Registration

Add to your Dotvvm.Startup.cs

```config.AddContribHeroIconConfiguration();```

## Sample Outline

```DOTHTML
<dc:HeroIcon ID="hardcoded" Icon="check_circle"  VisualStyle="Outline"/>
```

Renders:

```DOTHTML
<svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" stroke-width="2" id="hardcoded">  
    <path stroke-linecap="round" stroke-linejoin="round" d="M9 12.75L11.25 15 15 9.75M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
</svg>
```

<br />

## Sample Solid

```DOTHTML
<dc:HeroIcon ID="hardcoded" Icon="check_circle"  VisualStyle="Solid"/>
```

Renders:

```DOTHTML
<svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 24 24" id="hardcoded">  
    <path fill-rule="evenodd" d="M2.25 12c0-5.385 4.365-9.75 9.75-9.75s9.75 4.365 9.75 9.75-4.365 9.75-9.75 9.75S2.25 17.385 2.25 12zm13.36-1.814a.75.75 0 10-1.22-.872l-3.236 4.53L9.53 12.22a.75.75 0 00-1.06 1.06l2.25 2.25a.75.75 0 001.14-.094l3.75-5.25z" clip-rule="evenodd"></path>
</svg>
```

## Sample Mini

```DOTHTML
<dc:HeroIcon ID="hardcoded" Icon="check_circle"  VisualStyle="Mini"/>
```

Renders:

```DOTHTML
<svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20" id="hardcoded">  
    <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clip-rule="evenodd">
</path></svg>
```