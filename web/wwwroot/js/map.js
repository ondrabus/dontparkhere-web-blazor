var map = null;
var markerLayer = null;
var latitude = null;
var longitude = null;

function mapInit() {
    if (map !== null) {
        return;
    }

    if (latitude === null) {
        latitude = 49.199002;
    }
    if (longitude === null) {
        longitude = 16.606710;
    }

    var center = SMap.Coords.fromWGS84(longitude, latitude);
    map = new SMap(JAK.gel("m"), center, 15);

    map.addControl(new SMap.Control.Sync());
    map.addDefaultLayer(SMap.DEF_BASE).enable();

    var mouse = new SMap.Control.Mouse(SMap.MOUSE_PAN | SMap.MOUSE_WHEEL | SMap.MOUSE_ZOOM);
    map.addControl(mouse);

    markerLayer = new SMap.Layer.Marker();
    map.addLayer(markerLayer);
    markerLayer.enable();

    map.getSignals().addListener(window, "map-click", function (e, elm) {
        var coords = SMap.Coords.fromEvent(e.data.event, map);
        DotNet.invokeMethodAsync('DontParkHere', 'SetLocation', coords.y, coords.x, 1);
        mapCenter(coords.y, coords.x);
    });
}

function mapCenter(latitude, longitude) {
    if (map !== null) {
        map.setCenter(SMap.Coords.fromWGS84(longitude, latitude), true);
    }
}
