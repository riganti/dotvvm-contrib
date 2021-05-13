var PagerExtensions = (function () {
    function PagerExtensions() {
    }
    PagerExtensions.goToPage = function (set, index, loader) {
        var unwrappedSet = ko.unwrap(set);
        unwrappedSet.PagingOptions().PageIndex(index);
        loader()().then(function (newSet) {
            dotvvm.serialization.deserialize(newSet, unwrappedSet);
        });
    };
    return PagerExtensions;
}());
window.PagerExtensions = PagerExtensions;