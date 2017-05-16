import { BroadcastEventListener } from '../eventing/broadcast.event.listener';
export var SignalRConnectionMock = (function () {
    /**
     * @param {?} _mockErrors$
     * @param {?} _mockStatus$
     * @param {?} _listeners
     */
    function SignalRConnectionMock(_mockErrors$, _mockStatus$, _listeners) {
        this._mockErrors$ = _mockErrors$;
        this._mockStatus$ = _mockStatus$;
        this._listeners = _listeners;
    }
    Object.defineProperty(SignalRConnectionMock.prototype, "errors", {
        /**
         * @return {?}
         */
        get: function () {
            return this._mockErrors$;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(SignalRConnectionMock.prototype, "status", {
        /**
         * @return {?}
         */
        get: function () {
            return this._mockStatus$;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(SignalRConnectionMock.prototype, "id", {
        /**
         * @return {?}
         */
        get: function () {
            return 'xxxxxxxx-xxxx-xxxx-xxxxxxxxx';
        },
        enumerable: true,
        configurable: true
    });
    /**
     * @return {?}
     */
    SignalRConnectionMock.prototype.stop = function () {
    };
    /**
     * @return {?}
     */
    SignalRConnectionMock.prototype.start = function () {
        return Promise.resolve(null); // TODO: implement
    };
    /**
     * @param {?} method
     * @param {...?} parameters
     * @return {?}
     */
    SignalRConnectionMock.prototype.invoke = function (method) {
        var parameters = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            parameters[_i - 1] = arguments[_i];
        }
        return Promise.resolve(null);
    };
    /**
     * @param {?} listener
     * @return {?}
     */
    SignalRConnectionMock.prototype.listen = function (listener) {
        this._listeners[listener.event] = listener;
    };
    /**
     * @param {?} event
     * @return {?}
     */
    SignalRConnectionMock.prototype.listenFor = function (event) {
        var /** @type {?} */ listener = new BroadcastEventListener(event);
        this.listen(listener);
        return listener;
    };
    return SignalRConnectionMock;
}());
function SignalRConnectionMock_tsickle_Closure_declarations() {
    /** @type {?} */
    SignalRConnectionMock.prototype._mockErrors$;
    /** @type {?} */
    SignalRConnectionMock.prototype._mockStatus$;
    /** @type {?} */
    SignalRConnectionMock.prototype._listeners;
}
//# sourceMappingURL=signalr.connection.mock.js.map