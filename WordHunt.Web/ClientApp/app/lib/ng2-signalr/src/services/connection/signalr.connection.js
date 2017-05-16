import { BroadcastEventListener } from '../eventing/broadcast.event.listener';
import { ConnectionStatus } from './connection.status';
import { Subject } from 'rxjs/Subject';
export var SignalRConnection = (function () {
    /**
     * @param {?} jConnection
     * @param {?} jProxy
     * @param {?} zone
     */
    function SignalRConnection(jConnection, jProxy, zone) {
        this._jProxy = jProxy;
        this._jConnection = jConnection;
        this._zone = zone;
        this._errors = this.wireUpErrorsAsObservable();
        this._status = this.wireUpStatusEventsAsObservable();
    }
    Object.defineProperty(SignalRConnection.prototype, "errors", {
        /**
         * @return {?}
         */
        get: function () {
            return this._errors;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(SignalRConnection.prototype, "status", {
        /**
         * @return {?}
         */
        get: function () {
            return this._status;
        },
        enumerable: true,
        configurable: true
    });
    /**
     * @return {?}
     */
    SignalRConnection.prototype.start = function () {
        var _this = this;
        var /** @type {?} */ $promise = new Promise(function (resolve, reject) {
            _this._jConnection.start().done(function () {
                var results = [];
                for (var _i = 0; _i < arguments.length; _i++) {
                    results[_i - 0] = arguments[_i];
                }
                resolve(results);
            })
                .fail(function (err) {
                reject(err);
            });
        });
        return $promise;
    };
    /**
     * @return {?}
     */
    SignalRConnection.prototype.stop = function () {
        this._jConnection.stop();
    };
    Object.defineProperty(SignalRConnection.prototype, "id", {
        /**
         * @return {?}
         */
        get: function () {
            return this._jConnection.id;
        },
        enumerable: true,
        configurable: true
    });
    /**
     * @param {?} method
     * @param {...?} parameters
     * @return {?}
     */
    SignalRConnection.prototype.invoke = function (method) {
        var _this = this;
        var parameters = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            parameters[_i - 1] = arguments[_i];
        }
        if (method == null) {
            throw new Error('SignalRConnection: Failed to invoke. Argument \'method\' can not be null');
        }
        this.log("SignalRConnection. Start invoking '" + method + "'...");
        var /** @type {?} */ $promise = new Promise(function (resolve, reject) {
            (_a = _this._jProxy).invoke.apply(_a, [method].concat(parameters))
                .done(function (result) {
                _this.log("'" + method + "' invoked succesfully. Resolving promise...");
                resolve(result);
                _this.log("Promise resolved.");
            })
                .fail(function (err) {
                console.log("Invoking '" + method + "' failed. Rejecting promise...");
                reject(err);
                console.log("Promise rejected.");
            });
            var _a;
        });
        return $promise;
    };
    /**
     * @param {?} listener
     * @return {?}
     */
    SignalRConnection.prototype.listen = function (listener) {
        var _this = this;
        if (listener == null) {
            throw new Error('Failed to listen. Argument \'listener\' can not be null');
        }
        this.log("SignalRConnection: Starting to listen to server event with name " + listener.event);
        this._jProxy.on(listener.event, function () {
            var args = [];
            for (var _i = 0; _i < arguments.length; _i++) {
                args[_i - 0] = arguments[_i];
            }
            _this._zone.run(function () {
                var /** @type {?} */ casted = null;
                if (args.length === 0) {
                    return;
                }
                casted = (args[0]);
                _this.log('SignalRConnection.proxy.on invoked. Calling listener next() ...');
                listener.next(casted);
                _this.log('listener next() called.');
            });
        });
    };
    /**
     * @param {?} event
     * @return {?}
     */
    SignalRConnection.prototype.listenFor = function (event) {
        if (event == null || event === '') {
            throw new Error('Failed to listen. Argument \'event\' can not be empty');
        }
        var /** @type {?} */ listener = new BroadcastEventListener(event);
        this.listen(listener);
        return listener;
    };
    /**
     * @return {?}
     */
    SignalRConnection.prototype.wireUpErrorsAsObservable = function () {
        var _this = this;
        var /** @type {?} */ sError = new Subject();
        this._jConnection.error(function (error) {
            _this._zone.run(function () {
                sError.next(error);
            });
        });
        return sError;
    };
    /**
     * @return {?}
     */
    SignalRConnection.prototype.wireUpStatusEventsAsObservable = function () {
        var _this = this;
        var /** @type {?} */ sStatus = new Subject();
        var /** @type {?} */ connStatusNames = ['starting', 'received', 'connectionSlow', 'reconnecting', 'reconnected', 'stateChanged', 'disconnected'];
        // aggregate all signalr connection status handlers into 1 observable.
        connStatusNames.forEach(function (statusName) {
            // handler wire up, for signalr connection status callback.
            _this._jConnection[statusName](function () {
                var args = [];
                for (var _i = 0; _i < arguments.length; _i++) {
                    args[_i - 0] = arguments[_i];
                }
                _this._zone.run(function () {
                    sStatus.next(new ConnectionStatus(statusName));
                });
            });
        });
        return sStatus;
    };
    /**
     * @param {?} listener
     * @param {...?} args
     * @return {?}
     */
    SignalRConnection.prototype.onBroadcastEventReceived = function (listener) {
        var args = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            args[_i - 1] = arguments[_i];
        }
        this.log('SignalRConnection.proxy.on invoked. Calling listener next() ...');
        var /** @type {?} */ casted = null;
        if (args.length > 0) {
            casted = (args[0]);
        }
        this._zone.run(function () {
            listener.next(casted);
        });
        this.log('listener next() called.');
    };
    /**
     * @param {...?} args
     * @return {?}
     */
    SignalRConnection.prototype.log = function () {
        var args = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            args[_i - 0] = arguments[_i];
        }
        if (this._jConnection.logging === false) {
            return;
        }
        console.log(args.join(', '));
    };
    return SignalRConnection;
}());
function SignalRConnection_tsickle_Closure_declarations() {
    /** @type {?} */
    SignalRConnection.prototype._status;
    /** @type {?} */
    SignalRConnection.prototype._errors;
    /** @type {?} */
    SignalRConnection.prototype._jConnection;
    /** @type {?} */
    SignalRConnection.prototype._jProxy;
    /** @type {?} */
    SignalRConnection.prototype._zone;
}
//# sourceMappingURL=signalr.connection.js.map