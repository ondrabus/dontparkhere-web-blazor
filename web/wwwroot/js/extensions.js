window.blazorExtensions = {
    getCookie: function(name) {
        var value = "; " + document.cookie;
        var parts = value.split("; " + name + "=");
        if (parts.length === 2) return parts.pop().split(";").shift();
    },

    getCars: function() {
        let cookieData = this.getCookie("DPHCar");
        alert(cookieData);
        let cars = null;

        if (!cookieData) {
            cars = [];
        }
        else {
            cars = JSON.parse(cookieData);
        }
        alert(cars);

        return cars;
    },

    saveCar: function(car) {
        var date = new Date();
        date.setTime(date.getTime() + (365 * 24 * 60 * 60 * 1000));

        let existingCars = this.getCars();
        existingCars.push(car);

        document.cookie = "DPHCar=" + JSON.stringify(existingCars);
    }
};
