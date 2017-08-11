var reactHandler = ko.bindingHandlers["dotvvm-contrib-ReactBridge"] = {
    render: function ( el, Component, props ) {
        ReactDOM.render(
            React.createElement(Component,props),
            el
        );
    },
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var options = ko.unwrap(valueAccessor());
        var Component = ko.unwrap(options.component || options.$);
        var props = ko.toJS('props' in options ? options.props : viewModel);
 
        reactHandler.render(element, Component, props);
 
        return { controlsDescendantBindings: true };
        // TODO: init the control and subscribe to its events

    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var options = ko.unwrap(valueAccessor());
        var Component = ko.unwrap(options.component || options.$);
        var props = ko.toJS(options.props || viewModel);
 
        reactHandler.render(element, Component, props);
 
        return { controlsDescendantBindings: true };

        // TODO: update the control with a new value from the viewmodel
    }
};
