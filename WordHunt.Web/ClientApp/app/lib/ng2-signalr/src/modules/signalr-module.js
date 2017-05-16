import { NgModule, NgZone, OpaqueToken } from '@angular/core';
import { SignalR } from '../services/signalr';
var /** @type {?} */ SIGNALR_CONFIGURATION = new OpaqueToken('SIGNALR_CONFIGURATION');
/**
 * @param {?} configuration
 * @param {?} zone
 * @return {?}
 */
export function createSignalr(configuration, zone) {
    var /** @type {?} */ jConnectionFn = getJConnectionFn();
    return new SignalR(configuration, zone, jConnectionFn);
}
/**
 * @return {?}
 */
function getJConnectionFn() {
    var /** @type {?} */ jQuery = getJquery();
    var /** @type {?} */ hubConnectionFn = ((window)).jQuery.hubConnection;
    if (hubConnectionFn == null) {
        throw new Error('Signalr failed to initialize. Script \'jquery.signalR.js\' is missing. Please make sure to include \'jquery.signalR.js\' script.');
    }
    return hubConnectionFn;
}
/**
 * @return {?}
 */
function getJquery() {
    var /** @type {?} */ jQuery = ((window)).jQuery;
    if (jQuery == null) {
        throw new Error('Signalr failed to initialize. Script \'jquery.js\' is missing. Please make sure to include jquery script.');
    }
    return jQuery;
}
export var SignalRModule = (function () {
    function SignalRModule() {
    }
    /**
     * @param {?} getSignalRConfiguration
     * @return {?}
     */
    SignalRModule.forRoot = function (getSignalRConfiguration) {
        return {
            ngModule: SignalRModule,
            providers: [
                {
                    provide: SIGNALR_CONFIGURATION,
                    useFactory: getSignalRConfiguration
                },
                {
                    deps: [SIGNALR_CONFIGURATION, NgZone],
                    provide: SignalR,
                    useFactory: (createSignalr)
                }
            ],
        };
    };
    /**
     * @return {?}
     */
    SignalRModule.forChild = function () {
        throw new Error("forChild method not implemented");
    };
    SignalRModule.decorators = [
        { type: NgModule, args: [{
                    providers: [{
                            provide: SignalR,
                            useValue: SignalR
                        }]
                },] },
    ];
    /** @nocollapse */
    SignalRModule.ctorParameters = function () { return []; };
    return SignalRModule;
}());
function SignalRModule_tsickle_Closure_declarations() {
    /** @type {?} */
    SignalRModule.decorators;
    /**
     * @nocollapse
     * @type {?}
     */
    SignalRModule.ctorParameters;
}
//# sourceMappingURL=signalr-module.js.map