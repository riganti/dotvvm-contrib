ko.bindingHandlers.crystalReportFile = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var val = ko.unwrap(valueAccessor());
        setIframeSrc(element, val);

        var iframe = element.children[0];
        iframe.onload = resizeIframe;
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var val = ko.unwrap(valueAccessor());
        setIframeSrc(element, val);
    }
};

function setIframeSrc(element, crystalReportFile) {
    var iframe = element.children[0];
    var src = element.getAttribute("report-page-url") + "?CrystalReportFile=" + crystalReportFile;

    var queryParams = new Array();
    addQueryParam(queryParams, element, "DisplayToolbar", "display-toolbar");
    addQueryParam(queryParams, element, "DisplayStatusbar", "display-statusbar");
    addQueryParam(queryParams, element, "DisplayPage", "display-page");
    addQueryParam(queryParams, element, "BestFitPage", "best-fit-page");
    addQueryParam(queryParams, element, "ExtraCssFileUrl", "extra-css-file-url");
    addQueryParam(queryParams, element, "Width", "width");
    addQueryParam(queryParams, element, "Height", "height");

    for (let i = 0; i < queryParams.length; i++) {
        src += "&" + queryParams[i].name + "=" + queryParams[i].value;
    }

    iframe.setAttribute("src", src);
}

function addQueryParam(queryParams, element, paramName, attrName) {
    if (element.hasAttribute(attrName)) {
        queryParams.push({ name: paramName, value: element.getAttribute(attrName) });
    }
}

function resizeIframe() {
    var innerContent = this.contentWindow.document;
    resizeInnerContent(innerContent);

    var report = innerContent.body.querySelector("div[id$='__UI']");
    var reportStyle = window.getComputedStyle(report);
    this.setAttribute("width", reportStyle.getPropertyValue("width"));
    this.setAttribute("height", reportStyle.getPropertyValue("height"));
}

function resizeInnerContent(innerContent) {
    innerContent.body.style.margin = 0;

    var elements = innerContent.body.querySelectorAll("div[id$='__UI_mb'], div[id$='__UI_bc']");

    for (let i = 0; i < elements.length; i++) {
        elements[i].style.left = 0;
        elements[i].style.top = 0;
    }
}