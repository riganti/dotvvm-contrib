# GoogleMap

Google map control with support for showing specific **address** or **location** (longitude / latitude) and configuratable map **zoom**.

If you need any feature which is not currently supported, then please create an issue.

# Setup
It`s nessesary to provide your **Google API key**.  
Api key can be obtained using this [tutorial](https://developers.google.com/places/web-service/get-api-key).
### DotvvmStartup.cs => Configure
```C#
config.AddContribGoogleMapConfiguration(@"GOOGLE API KEY");
```

# Samples

## Sample 1: Address

Showing specific address

*Hardcoded*
```DOTHTML
<dc:GoogleMap Address="Chicken Dinner Rd Idaho 83607, USA" />
```

*Binding*
```DOTHTML
<dc:GoogleMap Address="{value: Address}" />
```

<br/>

## Sample 2: Longitude / Latitude

Showing specific location

*Hardcoded*
```DOTHTML
<dc:GoogleMap Longitude="-116.7723588" Latitude="43.5766682"/>
```

*Binding*
```DOTHTML
<dc:GoogleMap Longitude="{value: Longitude}" Latitude="{value: Latitude}"/>
```
<br />


## Sample 3: Zoom

Showing specific location

*Hardcoded*
```DOTHTML
<dc:GoogleMap Address="Chicken Dinner Rd Idaho 83607, USA" MapZoom="20"/>
```

*Binding*
```DOTHTML
<dc:GoogleMap Address="Chicken Dinner Rd Idaho 83607, USA" MapZoom="{value: Zoom}"/>
```
<br />