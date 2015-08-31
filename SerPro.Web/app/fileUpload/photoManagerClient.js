'use strict';
app.factory('photoManagerClient', photoManagerClient);

photoManagerClient.$inject = ['$resource'];

function photoManagerClient($resource) {
    return $resource("http://localhost:3846/api/photo/:fileName",
               { id: "@fileName" },
               {
                   'query': { method: 'GET', url: 'http://localhost:3846/api/photo/get' },
                   'save': { method: 'POST', url: 'http://localhost:3846/api/photo/post', transformRequest: angular.identity, headers: { 'Content-Type': undefined } },
                   'remove': { method: 'DELETE', url: 'http://localhost:3846/api/photo/delete/:fileName', params: { name: '@fileName' } }
               });
}
