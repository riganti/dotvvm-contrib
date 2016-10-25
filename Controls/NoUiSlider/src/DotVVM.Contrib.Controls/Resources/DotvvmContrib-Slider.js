/// <reference path="../scripts/DotVVM.d.ts"/>
/// <reference path="../scripts/typings/knockout/knockout.d.ts"/>
/// <reference path="../scripts/typings/nouislider/nouislider.d.ts"/>
var DotvvmContribSlider;
(function (DotvvmContribSlider) {
    function toogle(element, value) {
        var prop = ko.unwrap(value);
        var enabled = ko.isObservable(prop.enabled) ? prop.enabled() : prop.enabled;
        if (!enabled) {
            element.setAttribute('disabled', true);
        }
        else {
            element.removeAttribute('disabled');
        }
    }
    ko.bindingHandlers["dotvvm-contrib-slider"] = {
        init: function (element, valueAccessor, allBindingsAccesor, viewModel, bindingContext) {
            var model = viewModel;
            var prop = valueAccessor();
            noUiSlider.create(element, {
                start: [prop.value()],
                range: {
                    'min': prop.minValue,
                    'max': prop.maxValue
                },
                connect: false,
                step: prop.step,
                orientation: prop.orientation ? 'vertical' : 'horizontal',
                direction: prop.direction ? 'rtl' : 'ltr',
                format: {
                    to: function (value) {
                        return value;
                    },
                    from: function (value) {
                        return value;
                    }
                },
                pips: {
                    mode: 'positions',
                    values: [0, 50, 100],
                    density: 10,
                    stepped: true
                }
            });
            element.noUiSlider.on('change', function (values, handle) {
                var value = values[handle];
                prop.value(value);
            });
            toogle(element, prop);
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var prop = ko.unwrap(valueAccessor());
            element.noUiSlider.set([prop.value()]);
            toogle(element, prop);
        }
    };
    ko.bindingHandlers["dotvvm-contrib-switch"] = {
        init: function (element, valueAccessor, allBindingsAccesor, viewModel, bindingContext) {
            var model = viewModel;
            var prop = valueAccessor();
            noUiSlider.create(element, {
                start: prop.value() ? 1 : 0,
                range: {
                    'min': [0, 1],
                    'max': 1
                },
                connect: [true, false],
                step: 1,
                orientation: prop.orientation ? 'vertical' : 'horizontal',
                direction: prop.direction ? 'rtl' : 'ltr',
                format: {
                    to: function (value) {
                        return value;
                    },
                    from: function (value) {
                        return value;
                    }
                },
            });
            element.noUiSlider.on('change', function (values, handle) {
                var value = values[handle];
                prop.value(value ? true : false);
            });
            toogle(element, prop);
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var prop = ko.unwrap(valueAccessor());
            element.noUiSlider.set([prop.value() ? 1 : 0]);
            toogle(element, prop);
        }
    };
})(DotvvmContribSlider || (DotvvmContribSlider = {}));
//# sourceMappingURL=DotvvmContrib-Slider.js.map