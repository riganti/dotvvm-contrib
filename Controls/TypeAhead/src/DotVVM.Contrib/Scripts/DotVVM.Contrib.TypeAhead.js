ko.bindingHandlers["dotvvm-contrib-TypeAhead-DataSource"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

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
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.unwrap(valueAccessor());

        var data = $(element).data("typeahead-source");

        var displayMember = allBindingsAccessor.get("dotvvm-contrib-TypeAhead-DisplayMember");
        var displayMemberFunc = displayMember ? function (i) { return ko.unwrap(ko.unwrap(i)[displayMember]); } : function (i) { return ko.unwrap(i); };

        data.clear();
        data.add(value.map(function(item) { return displayMemberFunc(item); }));
    }
};

ko.bindingHandlers["dotvvm-contrib-TypeAhead-SelectedValue"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

        $(element).bind('typeahead:select blur', function (ev, suggestion) {

            if (!suggestion) {
                suggestion = $(element).val();
            }

            var data = ko.unwrap(allBindingsAccessor.get("dotvvm-contrib-TypeAhead-DataSource"));

            var valueMember = allBindingsAccessor.get("dotvvm-contrib-TypeAhead-ValueMember");
            var valueMemberFunc = valueMember ? function (i) { return ko.unwrap(ko.unwrap(i)[valueMember]); } : function (i) { return ko.unwrap(i); };
            var displayMember = allBindingsAccessor.get("dotvvm-contrib-TypeAhead-DisplayMember");
            var displayMemberFunc = displayMember ? function (i) { return ko.unwrap(ko.unwrap(i)[displayMember]); } : function (i) { return ko.unwrap(i); };

            for (var i = 0; i < data.length; i++) {
                if (displayMemberFunc(data[i]) === suggestion) {
                    valueAccessor()(valueMemberFunc(data[i]));
                    return;
                }
            }

            var limitToList = ko.unwrap(allBindingsAccessor.get("dotvvm-contrib-TypeAhead-LimitToList"));
            if (limitToList) {
                valueAccessor()(null);
            }                
            else {
                valueAccessor()(suggestion);
            }
        });

    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

        var selectedValue = ko.unwrap(valueAccessor());
        ko.delaySync.run(function () {

            var data = ko.unwrap(allBindingsAccessor.get("dotvvm-contrib-TypeAhead-DataSource"));

            var valueMember = allBindingsAccessor.get("dotvvm-contrib-TypeAhead-ValueMember");
            var valueMemberFunc = valueMember ? function (i) { return ko.unwrap(ko.unwrap(i)[valueMember]); } : function (i) { return ko.unwrap(i); };
            var displayMember = allBindingsAccessor.get("dotvvm-contrib-TypeAhead-DisplayMember");
            var displayMemberFunc = displayMember ? function (i) { return ko.unwrap(ko.unwrap(i)[displayMember]); } : function (i) { return ko.unwrap(i); };

            for (var i = 0; i < data.length; i++) {
                if (valueMemberFunc(data[i]) === selectedValue) {
                    $(element).typeahead('val', displayMemberFunc(data[i]));
                    return;
                }
            }

            var limitToList = ko.unwrap(allBindingsAccessor.get("dotvvm-contrib-TypeAhead-LimitToList"));
            if (limitToList) {
                $(element).typeahead('val', null);
            }       
        });

    },
    after: ["dotvvm-contrib-TypeAhead-DataSource"]
};
