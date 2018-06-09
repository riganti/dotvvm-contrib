# LoadablePanel

Renders a HTML &lt;div&gt; element containing Content that should be loaded in Load PostBack.
The Load PostBack is triggered immediately after the page is loaded.

## Sample 1: Basic Usage

You can do this:

```DOTHTML
@service service = Sample.Namespace.Service

<dc:LoadablePanel Load="{staticCommand: DataToBeLoaded = service.LoadData()}">
  <ContentTemplate>
    <dot:Literal Text="{value: DataToBeLoaded}"/>
  </ContentTemplate>
</dc:LoadablePanel>
```

<br />

## Sample 2: Something Else

You can do also this:

```DOTHTML
@service service = Sample.Namespace.Service

<dc:LoadablePanel Load="{staticCommand: DataToBeLoaded = service.LoadData()}" HideUntilLoaded="false">
  <ContentTemplate>
    <dot:Literal Text="{value: DataToBeLoaded == null ? "..." : DataToBeLoaded}"/>
  </ContentTemplate>
  <ProgressTempalte>
    <div class="update-progress">
        <div class="background"></div>
        <div class="spinner">
            <div class="rect1"></div><div class="rect2"></div><div class="rect3"></div><div class="rect4"></div><div class="rect5"></div>
        </div>
    </div>
  </ProgressTemplate>
</dc:LoadablePanel>
```