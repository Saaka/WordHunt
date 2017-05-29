import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { SignalRConnectionFactory } from '../../core/signalr/signalr-connection.factory';
import { ISignalRConnection, BroadcastEventListener } from 'ng2-signalr';
import { TeamChanged, FieldChecked, GameEnded } from "../game.models";

@Injectable()
export class GameHubService {

    private connection: ISignalRConnection;    
    private onSubscribed$: BroadcastEventListener<string>;
    private onTeamChanged$: BroadcastEventListener<TeamChanged>;
    private onFieldChecked$: BroadcastEventListener<FieldChecked>;
    private onGameEnded$: BroadcastEventListener<GameEnded>;

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

    public fieldChecked(onFieldChecked: (args: FieldChecked) => void) {
        if (!this.onFieldChecked$)
            this.onFieldChecked$ = this.connection.listenFor('FieldChecked');

        this.onFieldChecked$.subscribe(onFieldChecked);
    }

    public teamChanged(onTeamChanged: (args: TeamChanged) => void) {
        if (!this.onTeamChanged$)
            this.onTeamChanged$ = this.connection.listenFor('TeamChanged');

        this.onTeamChanged$.subscribe(onTeamChanged);
    }

    public gameEnded(onGameEnded: (args: GameEnded) => void) {
        if (!this.onGameEnded$)
            this.onGameEnded$ = this.connection.listenFor('GameEnded');

        this.onGameEnded$.subscribe(onGameEnded);
    }

    public subscribed(onSubscribed: (message: string) => void) {
        if (!this.onSubscribed$)
            this.onSubscribed$ = this.connection.listenFor('Subscribed');
        
        this.onSubscribed$.subscribe(onSubscribed);
    }

    disconnect() {
        if (this.onSubscribed$) {
            this.onSubscribed$.unsubscribe();
            this.onSubscribed$ = null;
        }
        if (this.onFieldChecked$) {
            this.onFieldChecked$.unsubscribe();
            this.onFieldChecked$ = null;
        }
        if (this.onTeamChanged$) {
            this.onTeamChanged$.unsubscribe();
            this.onTeamChanged$ = null;
        }
        if (this.onGameEnded$) {
            this.onGameEnded$.unsubscribe();
            this.onGameEnded$ = null;
        }
        if (this.connection) {
            this.connection.stop();
        }
    }
}