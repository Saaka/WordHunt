export var SignalRConfiguration = (function () {
    function SignalRConfiguration() {
        this.hubName = null;
        this.logging = false;
        this.qs = null;
        this.url = null;
        this.jsonp = false;
        this.withCredentials = false;
    }
    return SignalRConfiguration;
}());
function SignalRConfiguration_tsickle_Closure_declarations() {
    /**
     * connection url to the SignalR service
     * @type {?}
     */
    SignalRConfiguration.prototype.url;
    /**
     * Allows you to specify query string parameters object when the client connects
     * @type {?}
     */
    SignalRConfiguration.prototype.qs;
    /**
     * name of the SignalR service hub to connect to
     * @type {?}
     */
    SignalRConfiguration.prototype.hubName;
    /**
     * disable/enables client side logging. Defaults to false
     * @type {?}
     */
    SignalRConfiguration.prototype.logging;
    /**
     * Allows jsonp. This flag can be used to suppport CORS on older browsers
     * @type {?}
     */
    SignalRConfiguration.prototype.jsonp;
    /**
     * Allows withCredentials. This flag can be used to suppport CORS
     * @type {?}
     */
    SignalRConfiguration.prototype.withCredentials;
}
//# sourceMappingURL=signalr.configuration.js.map