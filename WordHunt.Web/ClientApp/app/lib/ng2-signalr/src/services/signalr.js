import { SignalRConfiguration } from './signalr.configuration';
import { SignalRConnection } from './connection/signalr.connection';
import { NgZone, Injectable } from '@angular/core';
export var SignalR = (function () {
    /**
     * @param {?} configuration
     * @param {?} zone
     * @param {?} jHubConnectionFn
     */
    function SignalR(configuration, zone, jHubConnectionFn) {
        this._configuration = configuration;
        this._zone = zone;
        this._jHubConnectionFn = jHubConnectionFn;
    }
    /**
     * @param {?=} options
     * @return {?}
     */
    SignalR.prototype.connect = function (options) {
        var _this = this;
        var /** @type {?} */ $promise = new Promise(function (resolve, reject) {
            var /** @type {?} */ configuration = _this.merge(options ? options : {});
            try {
                var /** @type {?} */ serialized = JSON.stringify(configuration.qs);
                if (configuration.logging) {
                    console.log("Connecting with...");
                    console.log("configuration:[url: '" + configuration.url + "'] ...");
                    console.log("configuration:[hubName: '" + configuration.hubName + "'] ...");
                    console.log("configuration:[qs: '" + serialized + "'] ...");
                }
            }
            catch (err) { }
            // create connection object
            var /** @type {?} */ jConnection = _this._jHubConnectionFn(configuration.url);
            jConnection.logging = configuration.logging;
            jConnection.qs = configuration.qs;
            // create a proxy
            var /** @type {?} */ jProxy = jConnection.createHubProxy(configuration.hubName);
            // !!! important. We need to register at least one on function otherwise server callbacks will not work.
            jProxy.on('noOp', function () { });
            var /** @type {?} */ hubConnection = new SignalRConnection(jConnection, jProxy, _this._zone);
            // start the connection
            console.log('Starting SignalR connection ...');
            jConnection.start({ withCredentials: configuration.withCredentials, jsonp: configuration.jsonp, transport: ['webSockets','longPolling'] })
                .done(function () {
                console.log('Connection established, ID: ' + jConnection.id);
                console.log('Connection established, Transport: ' + jConnection.transport.name);
                resolve(hubConnection);
            })
                .fail(function (error) {
                console.log('Could not connect');
                reject('Failed to connect. Error: ' + error.message); // ex: Error during negotiation request.
            });
        });
        return $promise;
    };
    /**
     * @param {?} overrides
     * @return {?}
     */
    SignalR.prototype.merge = function (overrides) {
        var /** @type {?} */ merged = new SignalRConfiguration();
        merged.hubName = overrides.hubName || this._configuration.hubName;
        merged.url = overrides.url || this._configuration.url;
        merged.qs = overrides.qs || this._configuration.qs;
        merged.logging = this._configuration.logging;
        merged.jsonp = overrides.jsonp || this._configuration.jsonp;
        merged.withCredentials = overrides.withCredentials || this._configuration.withCredentials;
        return merged;
    };
    SignalR.decorators = [
        { type: Injectable },
    ];
    /** @nocollapse */
    SignalR.ctorParameters = function () { return [
        { type: SignalRConfiguration, },
        { type: NgZone, },
        { type: Function, },
    ]; };
    return SignalR;
}());
function SignalR_tsickle_Closure_declarations() {
    /** @type {?} */
    SignalR.decorators;
    /**
     * @nocollapse
     * @type {?}
     */
    SignalR.ctorParameters;
    /** @type {?} */
    SignalR.prototype._configuration;
    /** @type {?} */
    SignalR.prototype._zone;
    /** @type {?} */
    SignalR.prototype._jHubConnectionFn;
}
//# sourceMappingURL=signalr.js.map