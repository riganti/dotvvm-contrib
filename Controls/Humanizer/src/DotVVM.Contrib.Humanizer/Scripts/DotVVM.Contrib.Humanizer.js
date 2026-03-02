var dotvvmContribHumanizer = (function () {
    function parseDateOnly(value) {
        // DateOnly format: "YYYY-MM-DD"
        var parts = value.split('-');
        return new Date(parseInt(parts[0]), parseInt(parts[1]) - 1, parseInt(parts[2]));
    }

    function humanizeDateTime(value) {
        if (!value) return '';

        var date;

        // TimeOnly format: starts with HH:MM (e.g. "14:30:00")
        if (/^\d{2}:\d{2}/.test(value) && value.indexOf('T') === -1 && value.indexOf('-') === -1) {
            return humanizeTimeOnly(value);
        }

        // DateOnly format: "YYYY-MM-DD" (no time component)
        if (/^\d{4}-\d{2}-\d{2}$/.test(value)) {
            date = parseDateOnly(value);
        } else {
            date = new Date(value);
        }

        if (!date || isNaN(date.getTime())) return '';

        return humanizeDateRelative(date);
    }

    function humanizeDateRelative(date) {
        var now = new Date();
        var diffMs = now - date;
        var diffSecs = Math.abs(diffMs) / 1000;
        var diffMins = diffSecs / 60;
        var diffHours = diffMins / 60;
        var diffDays = diffHours / 24;

        var isFuture = diffMs < 0;
        var text;

        if (diffSecs < 45) {
            return 'just now';
        } else if (diffSecs < 90) {
            text = 'a minute';
        } else if (diffMins < 45) {
            text = Math.round(diffMins) + ' minutes';
        } else if (diffMins < 90) {
            text = 'an hour';
        } else if (diffHours < 24) {
            text = Math.round(diffHours) + ' hours';
        } else if (diffHours < 48) {
            text = 'a day';
        } else if (diffDays < 30) {
            text = Math.round(diffDays) + ' days';
        } else if (diffDays < 45) {
            text = 'a month';
        } else if (diffDays < 345) {
            text = Math.round(diffDays / 30) + ' months';
        } else if (diffDays < 545) {
            text = 'a year';
        } else {
            text = Math.round(diffDays / 365) + ' years';
        }

        return isFuture ? text + ' from now' : text + ' ago';
    }

    function humanizeTimeOnly(value) {
        // TimeOnly format: "HH:MM:SS" or "HH:MM:SS.fffffff"
        var match = /^(\d{2}):(\d{2}):(\d{2})/.exec(value);
        if (!match) return value;

        var now = new Date();
        var targetMs = (parseInt(match[1]) * 3600 + parseInt(match[2]) * 60 + parseInt(match[3])) * 1000;
        var nowMs = (now.getHours() * 3600 + now.getMinutes() * 60 + now.getSeconds()) * 1000;

        var diff = Math.abs(nowMs - targetMs);
        var isFuture = targetMs > nowMs;

        // Reuse the relative logic by creating a pseudo-Date
        var pseudoDate = new Date(now.getTime() - (isFuture ? -diff : diff));
        return humanizeDateRelative(pseudoDate);
    }

    function humanizeTimeSpan(value) {
        if (!value) return '';

        // TimeSpan formats:
        //   "HH:MM:SS"
        //   "HH:MM:SS.fffffff"
        //   "d.HH:MM:SS"
        //   "d.HH:MM:SS.fffffff"
        //   Negative values have a leading "-"
        var match = /^(-)?(?:(\d+)\.)?(\d+):(\d+):(\d+)(?:\.\d+)?$/.exec(value);
        if (!match) return value;

        var negative = !!match[1];
        var days = match[2] ? parseInt(match[2]) : 0;
        var hours = parseInt(match[3]);
        var minutes = parseInt(match[4]);
        var seconds = parseInt(match[5]);

        var totalSeconds = days * 86400 + hours * 3600 + minutes * 60 + seconds;

        var text;
        if (totalSeconds === 0) {
            text = 'no time';
        } else if (totalSeconds < 60) {
            text = totalSeconds + (totalSeconds === 1 ? ' second' : ' seconds');
        } else if (totalSeconds < 3600) {
            var mins = Math.round(totalSeconds / 60);
            text = mins + (mins === 1 ? ' minute' : ' minutes');
        } else if (totalSeconds < 86400) {
            var hrs = Math.round(totalSeconds / 3600);
            text = hrs + (hrs === 1 ? ' hour' : ' hours');
        } else {
            var d = Math.round(totalSeconds / 86400);
            text = d + (d === 1 ? ' day' : ' days');
        }

        return negative ? 'minus ' + text : text;
    }

    return {
        humanizeDateTime: humanizeDateTime,
        humanizeTimeSpan: humanizeTimeSpan
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
