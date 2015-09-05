'use strict';
app.factory('photoManagerClient', photoManagerClient);

photoManagerClient.$inject = ['$resource'];

function photoManagerClient($resource) {

    var url = "http://localhost:3846/api/pictures/";

    return $resource(url + ":fileName",
               { id: "@fileName" },
               {
                   'query': { method: 'GET', url: url + 'get' },
                   'save': { method: 'POST', url: url + 'post', transformRequest: angular.identity, headers: { 'Content-Type': undefined } },
                   'remove': { method: 'DELETE', url: url + 'delete/:fileName', params: { name: '@fileName' } }
               });
}
