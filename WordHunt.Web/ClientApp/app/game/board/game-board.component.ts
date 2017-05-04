import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ISignalRConnection, BroadcastEventListener  } from 'ng2-signalr';

@Component({
    selector: 'game-board',
    templateUrl: './game-board.component.html',
    styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent implements OnInit, OnDestroy {

    @Input() gameId: number;

    connection: ISignalRConnection;

    onMessageSent$: BroadcastEventListener<string>;

    constructor(private route: ActivatedRoute) {
        console.log('ctor');
    }

    listen(message: string) {
        console.log(message);
    }

    ngOnInit() {
        console.log('ngOnInit');
        this.connection = this.route.snapshot.data['connection'];
        var connected = 'GameId: ' + this.gameId + ' Time: ' + new Date().toString();

        this.onMessageSent$ = this.connection.listenFor('HandleMessage');
        this.onMessageSent$.subscribe(this.listen);
        this.connection.invoke('HandleMessage', connected);
    }

    ngOnDestroy() {
        this.onMessageSent$.unsubscribe();
    }
}
