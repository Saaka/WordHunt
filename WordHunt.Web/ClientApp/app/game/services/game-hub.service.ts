import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { SignalRConnectionFactory } from '../../core/signalr/signalr-connection.factory';
import { ISignalRConnection, BroadcastEventListener } from 'ng2-signalr';

@Injectable()
export class GameHubService {

    private connection: ISignalRConnection;

    private onMessageSent$: BroadcastEventListener<string>;

    constructor(private connectionFactory: SignalRConnectionFactory) { }

    connect() {
        return Observable
            .fromPromise(this.connectionFactory
                .createConnection())
            .map((connection) => {
                this.connection = connection;
                return true;
            });
    }

    public messageReceived(onMessageReceived: (message: string) => void) {
        if (!this.onMessageSent$)
            this.onMessageSent$ = this.connection.listenFor('HandleMessage');
        this.onMessageSent$.subscribe(onMessageReceived);
    }

    sendMessage(message: string) {

        this.connection.invoke('HandleMessage', message + ' ConnectionId: ' + this.connection.id);
    }

    disconnect() {
        if (this.onMessageSent$)
            this.onMessageSent$.unsubscribe();
    }
}