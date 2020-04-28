function MyPropertyEditorController($scope) {
    function removeAllEntries() {
        alert('The model value is ' + $scope.model.value);
    }

    var removeAllEntriesAction = {
        labelKey: 'MyPropertyEditor_labelForMyAction',
        labelTokens: [],
        icon: 'heart',
        method: removeAllEntries,
        isDisabled: false
    };


    var propertyActions = [
        removeAllEntriesAction
    ];

    if ($scope.umbProperty) {
        $scope.umbProperty.setPropertyActions(propertyActions);
    }

}

angular.module("umbraco").controller("MyPropertyEditorController", MyPropertyEditorController);