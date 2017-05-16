export var ConnectionStatus = (function () {
    /**
     * @param {?} name
     */
    function ConnectionStatus(name) {
        if (name == null || name === "") {
            throw new Error("Failed to create ConnectionStatus. Argument 'name' can not be null or empty.");
        }
        this._name = name;
    }
    Object.defineProperty(ConnectionStatus.prototype, "name", {
        /**
         * @return {?}
         */
        get: function () {
            return this._name;
        },
        enumerable: true,
        configurable: true
    });
    /**
     * @return {?}
     */
    ConnectionStatus.prototype.toString = function () {
        return this._name;
    };
    /**
     * @param {?} other
     * @return {?}
     */
    ConnectionStatus.prototype.equals = function (other) {
        if (other == null) {
            return false;
        }
        return this._name === other.name;
    };
    return ConnectionStatus;
}());
function ConnectionStatus_tsickle_Closure_declarations() {
    /** @type {?} */
    ConnectionStatus.prototype._name;
}
//# sourceMappingURL=connection.status.js.map