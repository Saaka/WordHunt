import { Resolve } from '@angular/router';
import { SignalR, SignalRConnection } from '../../lib/ng2-signalr';
import { Injectable } from '@angular/core';

@Injectable()
export class SignalRConnectionResolver implements Resolve<SignalRConnection> {

    constructor(private _signalR: SignalR) { }

    resolve() {
        console.log('ConnectionResolver. Resolving...');
        return this._signalR.connect();
    }
}