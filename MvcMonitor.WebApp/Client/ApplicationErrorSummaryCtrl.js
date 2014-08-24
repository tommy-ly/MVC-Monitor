app.controller('ApplicationErrorSummaryCtrl', function($scope) {

    $(function() {
        var proxy = $.connection.showcaseErrorHub;

        function init() {
            proxy.server.getApplicationErrorSummary().done(function(summary) {
                $scope.$apply(function() {
                    $scope.applicationsSummary = summary;
                });
            });

            proxy.server.getErrorLocations().done(function (locationCount) {
                $scope.$apply(function() {
                    $scope.errorLocations = locationCount;
                });
            });
        }

        proxy.client.errorReceived = function (error) {
            init();
        };

        function connectionStateChanged(state) {
            var connectionStates = { 0: 'Connecting', 1: 'Connected', 2: 'Reconnecting', 4: 'Disconnected' };

            $scope.connectionState = connectionStates[state.newState];

            $scope.$apply(function() {
                if (state.newState == 1) {
                    $scope.connectionErrorDisplay = false;
                } else {
                    $scope.connectionErrorDisplay = true;
                }
            });
        }

        try {
            proxy.connection.stateChanged(connectionStateChanged);
            $.connection.hub.start().done(init);
        } catch(e) {
            alert(e.message);
        }
    });
});