angular.module('mvc-monitor').filter("parseAsDate", function () {
    return function (dateString) {
        if (!dateString || dateString == '')
            return null;

        return new Date(dateString);
    }
});