ko.bindingHandlers["dotvvm-contrib-GoogleMap-Address"] = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        element.Map = new google.maps.Map(element);
    },
    update: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        const address = ko.unwrap(valueAccessor());
        const geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': address },
            function(results, status) {
                if (status === google.maps.GeocoderStatus.OK) {

                    const zoom = parseInt(ko.unwrap(allBindingsAccessor()["dotvvm-contrib-GoogleMap-MapZoom"]));
                    dotvvmContribGoogleMapSetMap(element.Map,
                        { lat: results[0].geometry.location.lat(), lng: results[0].geometry.location.lng() },
                        zoom);
                } else {
                    console.error(`Geocode was not successful for the following reason: ${status}`);
                }
            });
    }
};

ko.bindingHandlers["dotvvm-contrib-GoogleMap-Longitude"] = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        if (element.Map === undefined)
            element.Map = new google.maps.Map(element);
    },
    update: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        const lng = parseFloat(ko.unwrap(valueAccessor()));
        const lat = parseFloat(ko.unwrap(allBindingsAccessor()["dotvvm-contrib-GoogleMap-Latitude"]));
        const zoom = parseInt(ko.unwrap(allBindingsAccessor()["dotvvm-contrib-GoogleMap-MapZoom"]));
        dotvvmContribGoogleMapSetMap(element.Map, { lat: lat, lng: lng }, zoom);
    }
};

ko.bindingHandlers["dotvvm-contrib-GoogleMap-Latitude"] = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        if (element.Map === undefined)
            element.Map = new google.maps.Map(element);
    },
    update: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        const lat = parseFloat(ko.unwrap(valueAccessor()));
        const lng = parseFloat(ko.unwrap(allBindingsAccessor()["dotvvm-contrib-GoogleMap-Longitude"]));
        const zoom = parseInt(ko.unwrap(allBindingsAccessor()["dotvvm-contrib-GoogleMap-MapZoom"]));
        dotvvmContribGoogleMapSetMap(element.Map, { lat: lat, lng: lng }, zoom);
    }
};

ko.bindingHandlers["dotvvm-contrib-GoogleMap-MapZoom"] = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        if (element.Map === undefined)
            element.Map = new google.maps.Map(element);

        if (ko.isWritableObservable(valueAccessor())) {

            element.Map.addListener("zoom_changed",
                function() {
                    valueAccessor()(element.Map.getZoom());
                });
        }
    },
    update: function(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        element.Map.setZoom(ko.unwrap(valueAccessor()));
    }
};

function dotvvmContribGoogleMapSetMap(map, location, zoom) {
    map.setCenter({ lat: location.lat, lng: location.lng });
    map.setZoom(zoom);
    const marker = new google.maps.Marker();
    marker.setPosition(location);
    marker.setMap(map);
}