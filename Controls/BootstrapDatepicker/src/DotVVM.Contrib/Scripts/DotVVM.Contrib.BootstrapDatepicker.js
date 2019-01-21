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
                    prop(dotvvm.serialization.serializeDate(e.date, false));
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
