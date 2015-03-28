app.controller('ApplicationErrorSummaryCtrl', function($scope) {
    var signalrConnectionStates = { 0: 'Connecting', 1: 'Connected', 2: 'Reconnecting', 4: 'Disconnected' };

    var proxy = $.connection.showcaseErrorHub;

    function refresh() {
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
        refresh();
    };

    function connectionStateChanged(state) {
        console.log('Connection state changed to ' + signalrConnectionStates[state.newState]);

        if (state.newState == 4) {
            console.log('Restarting hub...');
            connectToHub();
        }

        $scope.connectionState = signalrConnectionStates[state.newState];
    }

    function connectToHub() {
        try {
            $.connection.hub.start().done(refresh);
        } catch (e) {
            alert(e.message);
        }
    }

    proxy.connection.stateChanged(connectionStateChanged);
    connectToHub();
});