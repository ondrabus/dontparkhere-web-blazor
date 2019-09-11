function getCookie (name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length === 2) return parts.pop().split(";").shift();
}

// get cookie from browser / request
function getCars() {
    let cookieData = getCookie("DPH");
    let cars = [];

    if (cookieData) {
        cars = JSON.parse(cookieData);
    }

    return cars;
}


// set cookie with data from Blazor
function setCar(car) {
    let date = new Date();
    date.setTime(date.getTime() + (365 * 24 * 60 * 60 * 1000));

    let existingCars = getCars();
    existingCars.push(car);

    document.cookie = "DPH=" + JSON.stringify(existingCars);
}