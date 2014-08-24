app.controller('ErrorIndexCtrl', function($scope, $filter) {
    var connected = false;

    var proxy;

    var currentPage = 1;
    var pageSize = 20;


    var nextPage = function() {
        currentPage = currentPage + 1;
        refresh();
    };

    var prevPage = function() {
        currentPage = currentPage - 1;

        if (currentPage < 1) {
            currentPage = 1;
        }

        refresh();
    };

    var refresh = function() {
        proxy.server.getPagedErrors(currentPage, pageSize, $scope.filterFrom, $scope.filterTo, $scope.filterUser, $scope.filterApplication, $scope.filterLocation)
            .done(function(pagedErrors) {
                $scope.$apply(function() {
                    var maxShowing = ((pagedErrors.Page + 1) * pagedErrors.PageSize);

                    if (maxShowing > pagedErrors.TotalCount) {
                        maxShowing = pagedErrors.TotalCount;
                    }

                    $scope.showing = ((pagedErrors.Page) * pagedErrors.PageSize) + ' to ' + maxShowing;

                    $scope.pagedErrors = pagedErrors;
                    $scope.currentPage = currentPage;
                });
            });
    };

    var refreshOnInputStopped = function() {
        proxy.server.getPagedErrors(currentPage, pageSize, $scope.filterFrom, $scope.filterTo, $scope.filterUser, $scope.filterApplication, $scope.filterLocation)
            .done(function (pagedErrors) {
                $scope.$apply(function () {
                    var maxShowing = ((pagedErrors.Page + 1) * pagedErrors.PageSize);

                    if (maxShowing > pagedErrors.TotalCount) {
                        maxShowing = pagedErrors.TotalCount;
                    }

                    $scope.showing = ((pagedErrors.Page) * pagedErrors.PageSize) + ' to ' + maxShowing;

                    $scope.pagedErrors = pagedErrors;
                    $scope.currentPage = currentPage;
                });
            });
    };

    $scope.errorSelected = function(error) {
        $scope.selectedError = error;
    };

    $(function() {
        proxy = $.connection.showcaseErrorHub;

        function init() {
            $scope.nextPage = nextPage;
            $scope.prevPage = prevPage;

            var thirtyDaysAgo = new Date(new Date().setDate(new Date().getDate() - 5));

            $scope.filterFrom = $filter('date')(thirtyDaysAgo, 'dd/MM/yyyy');
            $scope.filterTo = $filter('date')(new Date(new Date().setDate(new Date().getDate() + 1)), 'dd/MM/yyyy');
            $scope.filterLocation = "";
            $scope.filterUser = "";
            $scope.filterApplication = "";

            $scope.refresh = refresh;
            $scope.refreshOnInputStopped = refreshOnInputStopped;

            connected = true;
            refresh();
        }

        try {
            $.connection.hub.start().done(init);
        } catch(e) {
            alert(e.message);
        }

        if (connected) {
            refresh();
        }
    });
});