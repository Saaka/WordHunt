import { ISignalRConnection } from './connection/i.signalr.connection';
import { SignalRConfiguration } from './signalr.configuration';
import { NgZone } from '@angular/core';
import { IConnectionOptions } from './connection/connection.options';
export declare class SignalR {
    private _configuration;
    private _zone;
    private _jHubConnectionFn;
    constructor(configuration: SignalRConfiguration, zone: NgZone, jHubConnectionFn: Function);
    connect(options?: IConnectionOptions): Promise<ISignalRConnection>;
    private merge(overrides);
}
