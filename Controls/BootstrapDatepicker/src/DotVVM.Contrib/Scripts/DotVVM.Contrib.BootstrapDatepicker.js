ko.bindingHandlers["dotvvm-contrib-BootstrapDatepicker"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        $el = $(element);
        $inputGroup = $el.parent();

        if ($inputGroup.is('.input-group.date'))
            $el = $inputGroup;

        $el.datepicker()
            .on('changeDate', function (e) {
                var prop = valueAccessor();

                if (ko.isObservable(prop)) {
                    var v = dotvvm.serialization.serializeDate(e.date, false);
                    if (v !== prop()) { // prevents multiple viewmodel updates
                        prop(v);
                    }
                }
            })
            .on('change', function (e) {
                if (!$(element).val()) {

                    var prop = valueAccessor();
                    if (ko.isObservable(prop)) {
                        prop(null);
                    }
                }
            }).
            on('blur', function (e) { // hotfix for https://github.com/uxsolutions/bootstrap-datepicker/issues/2325
                var prop = valueAccessor();

                if (ko.isObservable(prop)) {
                    var v = dotvvm.serialization.serializeDate($(element).datepicker('getDate'), false);
                    if (prop() !== v) { // prevents multiple viewmodel updates
                        prop(v);
                    }
                }
            });

    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.unwrap(valueAccessor());

        if (typeof value === "string") {
            value = dotvvm.globalize.parseDotvvmDate(value);
        }
        
        if (value instanceof Date) {
            $el = $(element);
            $inputGroup = $el.parent();

            if ($inputGroup.is('.input-group.date'))
                $el = $inputGroup;

            $el.datepicker('update', value);
        } else {
            $el.val("");
        }
    }
};
