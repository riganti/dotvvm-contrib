ko.bindingHandlers["dotvvm-contrib-FileUploadNoIframe"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

        var args = ko.unwrap(valueAccessor());

        function reportProgress(isBusy, percent, resultOrError) {
            dotvvm.fileUpload.reportProgress(element.parentElement.attributes["data-dotvvm-upload-id"], isBusy, percent, resultOrError);
        }

        element.addEventListener("change", function() {
            var xhr = XMLHttpRequest ? new XMLHttpRequest() : new (window["ActiveXObject"])("Microsoft.XMLHTTP");
            xhr.open("POST", args.url, true);
            xhr.setRequestHeader("X-DotVVM-AsyncUpload", "true");
            xhr.upload.onprogress = function (e) {
                if (e.lengthComputable) {
                    reportProgress(true, Math.round(e.loaded * 100 / e.total, 0), '');
                }
            };
            xhr.onload = function (e) {
                if (xhr.status == 200) {
                    reportProgress(false, 100, JSON.parse(xhr.responseText));
                    element.value = "";
                } else {
                    reportProgress(false, 0, "Upload failed.");
                }
            };

            var formData = new FormData();
            if (element.files.length > 1) {
                for (var i = 0; i < element.files.length; i++) {
                    formData.append("upload[]", element.files[i]);
                }
            } else if (element.files.length > 0) {
                formData.append("upload", element.files[0]);
            }
            xhr.send(formData);
        });

    }
};
