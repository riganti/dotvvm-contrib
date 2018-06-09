# LoadablePanel

Renders a HTML &lt;div&gt; element containing Content that should be loaded in Load PostBack.
The Load PostBack is triggered immediately after the page is loaded. The Content of the control
is hidden until the PostBack finishes.

This control may be useful when you want to load different parts of page asynchronously.

## Sample 1: Basic Usage

```DOTHTML
@service service = Sample.Namespace.Service

<dc:LoadablePanel Load="{staticCommand: DataToBeLoaded = service.LoadData()}">
  <ContentTemplate>
    <dot:Literal Text="{value: DataToBeLoaded}"/>
  </ContentTemplate>
</dc:LoadablePanel>
```

<br />

## Sample 2: Progress Template

```DOTHTML
@service service = Sample.Namespace.Service

<dc:LoadablePanel Load="{staticCommand: DataToBeLoaded = service.LoadData()}" HideUntilLoaded="false">
  <ContentTemplate>
    <dot:Literal Text="{value: DataToBeLoaded == null ? "..." : DataToBeLoaded}"/>
  </ContentTemplate>
  <ProgressTemplate>
    <div class="update-progress">
        <div class="background"></div>
        <div class="spinner">
            <div class="rect1"></div><div class="rect2"></div><div class="rect3"></div><div class="rect4"></div><div class="rect5"></div>
        </div>
    </div>
  </ProgressTemplate>
</dc:LoadablePanel>
```