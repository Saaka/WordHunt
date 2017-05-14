import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { SignalRConnectionFactory } from '../../core/signalr/signalr-connection.factory';
import { ISignalRConnection, BroadcastEventListener } from 'ng2-signalr';

@Injectable()
export class GameHubService {

    private connection: ISignalRConnection;    
    private onSubscribed$: BroadcastEventListener<string>;

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

    public subscribeToGame(gameId: number) : Promise<string> {
        return this.connection
            .invoke("Subscribe", gameId);
    }

    public subscribed(onSubscribed: (message: string) => void) {
        if (!this.onSubscribed$)
            this.onSubscribed$ = this.connection.listenFor('Subscribed');


        this.onSubscribed$.subscribe(onSubscribed);
    }

    disconnect() {
        if (this.onSubscribed$)
            this.onSubscribed$.unsubscribe();
    }
}