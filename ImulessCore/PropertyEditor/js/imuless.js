angular.module('umbraco').controller('ImulessController', function ($scope, $http) {

    //$scope.model.value = "";

    $http.get("/App_Plugins/Imuless/js/imuless.config.js").then(function (config) {
        $scope.model.config.themes = config.data;

        init();
    });

    //load model or default
    $scope.model.value = $scope.model.value || {
        theme: "",
        vars: []
    }

    //used to hold dropdown values for ng-select workings
    $scope.tempVars = [];

    var getThemeConfig = function () {
        return _.find($scope.model.config.themes, function (config) {
            return config.name == $scope.model.value.theme;
        });
    }

    $scope.setThemeConfig = function () {
        $scope.selectedTheme = getThemeConfig();

        //console.log($scope.model.value);
    }

    $scope.clearTheme = function () {
        $scope.model.value.vars = [];
        $scope.tempVars = [];
    }

    $scope.updateVarModel = function (index, key) {
        $scope.model.value.vars[index] = { alias: key, value: $scope.tempVars[index] }
    }

    var initTempVars = function () {
        for (var i in $scope.model.value.vars) {

            if (!$scope.model.value.vars[i]) {
                $scope.model.value.vars[i].value = "";
            }

            $scope.tempVars.push($scope.model.value.vars[i].value);
        }
    }

    //init
    var init = function () {
        $scope.setThemeConfig();
        initTempVars();
    }
});