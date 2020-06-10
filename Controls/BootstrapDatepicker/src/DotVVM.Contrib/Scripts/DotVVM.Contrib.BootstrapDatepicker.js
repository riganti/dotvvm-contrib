ko.bindingHandlers["dotvvm-contrib-BootstrapDatepicker"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var $el = $(element);
        var $inputGroup = $el.parent();

        if ($inputGroup.is('.input-group.date'))
            $el = $inputGroup;

        var callback = $el.attr("data-dotvvm-contrib-BootstrapDatepicker-changed");
        $el.datepicker()
            .on('changeDate', function (e) {
                var prop = valueAccessor();

                if (ko.isObservable(prop)) {
                    var v = dotvvm.serialization.serializeDate(e.date, false);
                    if (v !== prop()) { // prevents multiple viewmodel updates
                        prop(v);

                        if (callback != undefined) {
                            new Function(callback).call($el[0]);
                        }
                    }   
                }
            })
            .on('change blur', function (e) { // hotfix for https://github.com/uxsolutions/bootstrap-datepicker/issues/2325
                var prop = valueAccessor();

                if (ko.isObservable(prop)) {
                    var v = dotvvm.serialization.serializeDate($el.datepicker('getDate'), false);
                    if (prop() !== v) { // prevents multiple viewmodel updates
                        prop(v);
                        if (callback != undefined) {
                            new Function(callback).call($el[0]);
                        }
                    }
                }
            });

    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.unwrap(valueAccessor());

        if (typeof value === "string") {
            value = dotvvm.globalize.parseDotvvmDate(value);
        }
        
        var $el = $(element);
        if (value instanceof Date) {
            var $inputGroup = $el.parent();

            if ($inputGroup.is('.input-group.date'))
                $el = $inputGroup;

            $el.datepicker('update', value);
        } else {
            $el.val("");
        }
    }
};
