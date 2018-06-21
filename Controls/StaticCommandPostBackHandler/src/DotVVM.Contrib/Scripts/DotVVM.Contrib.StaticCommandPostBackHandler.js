dotvvm.events.init.subscribe(function () {
    dotvvm.postbackHandlers["StaticCommandPostBackHandler"] = function ConfirmPostBackHandler(options) {

        var command = options.beforePostBack;
        return {
            execute: function (callback, opt) {
                return new Promise(function (resolve, reject) {
                    var $element = opt.sender;

                    eval(command)().then(callback).then(resolve, reject);
                });
            },
        };
    };
});