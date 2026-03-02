// Initialize dayjs with the relativeTime plugin
dayjs.extend(dayjs_plugin_relativeTime);

var dotvvmContribHumanizer = (function () {
    function getLocale() {
        var culture = dotvvm.getCulture ? dotvvm.getCulture() : '';
        // DotVVM culture is e.g. "en-US", "cs-CZ", "zh-TW"
        // dayjs uses lowercase locale names like "en", "cs", "zh-tw"
        var lang = culture.toLowerCase().replace('_', '-');
        return lang || 'en';
    }

    function humanizeDateTime(value) {
        if (!value) return '';
        var date = dotvvm.serialization.parseDate(value);
        if (!date) return '';

        return dayjs(date).locale(getLocale()).fromNow();
    }

    return {
        humanizeDateTime: humanizeDateTime
    };
})();

ko.bindingHandlers["dotvvm-contrib-HumanizeDateTime"] = {
    init: function (element, valueAccessor) {
        var options = ko.unwrap(valueAccessor());
        if (ko.unwrap(options.autoUpdate)) {
            var interval = setInterval(function () {
                ko.bindingHandlers["dotvvm-contrib-HumanizeDateTime"].update(element, valueAccessor);
            }, 60000);
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                clearInterval(interval);
            });
        }
    },
    update: function (element, valueAccessor) {
        var options = valueAccessor();
        var value = ko.unwrap(ko.unwrap(options).value);
        element.textContent = value ? dotvvmContribHumanizer.humanizeDateTime(value) : '';
    }
};
