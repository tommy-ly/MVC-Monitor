var app = angular.module('mvc-monitor', []);

app.directive('datepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function(scope, element, attrs, ngModelCtrl) {
            $(function() {
                element.datepicker({
                    dateFormat: 'dd/mm/yy',
                    onSelect: function(date) {
                        ngModelCtrl.$setViewValue(date);
                        scope.$apply();
                    }
                });
            });
        }
    };
});

app.directive('ngEnter', function() {
    return function(scope, element, attrs) {
        element.bind("keydown keypress", function(event) {
            if(event.which === 13) {
                scope.$apply(function(){
                    scope.$eval(attrs.ngEnter, {'event': event});
                });

                event.preventDefault();
            }
        });
    };
});

app.filter("parseAsDate", function () {
    return function (dateString) {
        if (!dateString || dateString == '')
            return null;

        return new Date(dateString);
    }
});

app.filter('trim', function () {
    return function (value, wordwise, max, tail) {
        if (!value) return '';

        max = parseInt(max, 10);
        if (!max) return value;
        if (value.length <= max) return value;

        value = value.substr(0, max);
        if (wordwise) {
            var lastspace = value.lastIndexOf(' ');
            if (lastspace != -1) {
                value = value.substr(0, lastspace);
            }
        }

        return value + (tail || ' …');
    };
});