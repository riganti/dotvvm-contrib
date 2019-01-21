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
                    prop(e.date);
                }
            })
            .on('change', function (e) {
                if (!$(element).val()) {

                    var prop = valueAccessor();
                    if (ko.isObservable(prop)) {
                        prop(null);
                    }
                }
            });

    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.unwrap(valueAccessor());

        if (value && typeof value === "string") {
            value = new Date(value);
        }

        if (value) {
            $el = $(element);
            $inputGroup = $el.parent();

            if ($inputGroup.is('.input-group.date'))
                $el = $inputGroup;

            $el.datepicker('update', value);
        }
    }
};
