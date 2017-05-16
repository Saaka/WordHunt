import { SignalR, ISignalRConnection } from '../../lib/ng2-signalr';
import { Injectable } from '@angular/core';

@Injectable()
export class SignalRConnectionFactory {

    constructor(private _signalR: SignalR) { }

    createConnection(): Promise<ISignalRConnection> {
        return this._signalR.connect();
    }
}