import { ModuleWithProviders, NgZone } from '@angular/core';
import { SignalR } from '../services/signalr';
import { SignalRConfiguration } from '../services/signalr.configuration';
export declare function createSignalr(configuration: SignalRConfiguration, zone: NgZone): SignalR;
export declare class SignalRModule {
    static forRoot(getSignalRConfiguration: Function): ModuleWithProviders;
    static forChild(): ModuleWithProviders;
}
