import { ConnectionStatus } from './connection.status';
export var ConnectionStatuses = (function () {
    function ConnectionStatuses() {
    }
    Object.defineProperty(ConnectionStatuses, "starting", {
        /**
         * @return {?}
         */
        get: function () {
            return ConnectionStatuses.statuses[0];
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ConnectionStatuses, "received", {
        /**
         * @return {?}
         */
        get: function () {
            return ConnectionStatuses.statuses[1];
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ConnectionStatuses, "connectionSlow", {
        /**
         * @return {?}
         */
        get: function () {
            return ConnectionStatuses.statuses[2];
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ConnectionStatuses, "reconnecting", {
        /**
         * @return {?}
         */
        get: function () {
            return ConnectionStatuses.statuses[3];
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ConnectionStatuses, "reconnected", {
        /**
         * @return {?}
         */
        get: function () {
            return ConnectionStatuses.statuses[4];
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ConnectionStatuses, "stateChanged", {
        /**
         * @return {?}
         */
        get: function () {
            return ConnectionStatuses.statuses[5];
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ConnectionStatuses, "disconnected", {
        /**
         * @return {?}
         */
        get: function () {
            return ConnectionStatuses.statuses[6];
        },
        enumerable: true,
        configurable: true
    });
    ConnectionStatuses.statuses = [
        new ConnectionStatus("starting"),
        new ConnectionStatus("received"),
        new ConnectionStatus("connectionSlow"),
        new ConnectionStatus("reconnecting"),
        new ConnectionStatus("reconnected"),
        new ConnectionStatus("stateChanged"),
        new ConnectionStatus("disconnected"),
    ];
    return ConnectionStatuses;
}());
function ConnectionStatuses_tsickle_Closure_declarations() {
    /** @type {?} */
    ConnectionStatuses.statuses;
}
//# sourceMappingURL=connection.statuses.js.map