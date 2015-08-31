'use strict';
angular.module('AngularAuthApp').directive('egPhotoUploader', egPhotoUploader);

egPhotoUploader.$inject = ['appInfo', 'photoManager'];

function egPhotoUploader(appInfo, photoManager) {

    var directive = {
        link: link,
        restrict: 'E',
        templateUrl: 'app/fileUpload/egPhotoUploader.html',
        scope: true
    };
    return directive;

    function link(scope, element, attrs) {
        scope.hasFiles = false;
        scope.photos = [];
        scope.upload = photoManager.upload;
        scope.appStatus = appInfo.status;
        scope.photoManagerStatus = photoManager.status;
    }
}

