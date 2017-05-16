import { Subject } from 'rxjs/Subject';
import { SignalRConnectionMock } from './signalr.connection.mock';
export var SignalRConnectionMockManager = (function () {
    function SignalRConnectionMockManager() {
        this._errors$ = new Subject();
        this._status$ = new Subject();
        this._listeners = {};
        this._object = new SignalRConnectionMock(this._errors$, this._status$, this._listeners);
    }
    Object.defineProperty(SignalRConnectionMockManager.prototype, "mock", {
        /**
         * @return {?}
         */
        get: function () {
            return this._object;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(SignalRConnectionMockManager.prototype, "errors$", {
        /**
         * @return {?}
         */
        get: function () {
            return this._errors$;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(SignalRConnectionMockManager.prototype, "status$", {
        /**
         * @return {?}
         */
        get: function () {
            return this._status$;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(SignalRConnectionMockManager.prototype, "listeners", {
        /**
         * @return {?}
         */
        get: function () {
            return this._listeners;
        },
        enumerable: true,
        configurable: true
    });
    return SignalRConnectionMockManager;
}());
function SignalRConnectionMockManager_tsickle_Closure_declarations() {
    /** @type {?} */
    SignalRConnectionMockManager.prototype._status$;
    /** @type {?} */
    SignalRConnectionMockManager.prototype._errors$;
    /** @type {?} */
    SignalRConnectionMockManager.prototype._object;
    /** @type {?} */
    SignalRConnectionMockManager.prototype._listeners;
}
//# sourceMappingURL=signalr.connection.mock.manager.js.map