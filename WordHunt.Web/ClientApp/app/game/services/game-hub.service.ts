import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { SignalRConnectionFactory } from '../../core/signalr/signalr-connection.factory';
import { ISignalRConnection, BroadcastEventListener } from '../../lib/ng2-signalr';
import { TeamChanged } from "../game.models";

@Injectable()
export class GameHubService {

    private connection: ISignalRConnection;    
    private onSubscribed$: BroadcastEventListener<string>;
    private onTeamChanged$: BroadcastEventListener<TeamChanged>;

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

    public teamChanged(onTeamChanged: (args: TeamChanged) => void) {
        if (!this.onTeamChanged$)
            this.onTeamChanged$ = this.connection.listenFor('TeamChanged');

        this.onTeamChanged$.subscribe(onTeamChanged);
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