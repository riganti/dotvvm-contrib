(function() {

function getAccessors(allBindings) {
    var itemValue = allBindings.get("dotvvm-contrib-TypeAhead-ItemValue");
    var itemValueFunc = itemValue ? function (i) { return ko.unwrap(itemValue(ko.unwrap(i))); } : function (i) { return ko.unwrap(i); };
    var itemText = allBindings.get("dotvvm-contrib-TypeAhead-ItemText");
    var itemTextFunc = itemText ? function (i) { return ko.unwrap(itemText(ko.unwrap(i))); } : function (i) { return ko.unwrap(i); };    

    var data = ko.unwrap(allBindings.get("dotvvm-contrib-TypeAhead-DataSource"));

    return [ data, itemValueFunc, itemTextFunc ]
}

ko.bindingHandlers["dotvvm-contrib-TypeAhead-DataSource"] = {
    init: function (element, valueAccessor, allBindingsAccessor) {

        var source = new Bloodhound({
            local: [],
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            datumTokenizer: Bloodhound.tokenizers.whitespace
        });

        $(element).typeahead({
                minLength: 1,
                highlight: true
            },
            {
                name: 'default',
                source: source
            })
            .on('keydown', function (e) {
                if (e.which === 13) {
                    e.preventDefault();

                    // click on the first selected item
                    $(element).parent().find(".tt-menu .tt-selectable:visible:first-child").click();
                }
            });

        $(element).data("typeahead-source", source);

    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = ko.unwrap(valueAccessor());

        var data = $(element).data("typeahead-source");

        var [ _, itemValue, itemText ] = getAccessors(allBindingsAccessor)

        data.clear();
        data.add(value.map(function(item) { return itemText(item); }));
    }
};

ko.bindingHandlers["dotvvm-contrib-TypeAhead-SelectedValue"] = {
    init: function (element, valueAccessor, allBindingsAccessor) {

        $(element).bind('typeahead:select blur', function (ev, suggestion) {

            if (!suggestion) {
                suggestion = $(element).val();
            }

            var [ data, itemValue, itemText ] = getAccessors(allBindingsAccessor)

            for (var i = 0; i < data.length; i++) {
                if (itemText(data[i]) === suggestion) {
                    valueAccessor()(itemValue(data[i]));
                    return;
                }
            }
            valueAccessor()(null);

        });

    },
    update: function (element, valueAccessor, allBindingsAccessor) {

        var selectedValue = ko.unwrap(valueAccessor());
        ko.delaySync.run(function () {

            var [ data, itemValue, itemText ] = getAccessors(allBindingsAccessor)

            for (var i = 0; i < data.length; i++) {
                if (itemValue(data[i]) === selectedValue) {
                    $(element).typeahead('val', itemText(data[i]));
                    return;
                }
            }
            $(element).typeahead('val', null);
        });

    },
    after: ["dotvvm-contrib-TypeAhead-DataSource"]
};

}())
