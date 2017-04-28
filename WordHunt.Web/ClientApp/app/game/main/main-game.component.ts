import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'main-game',
    templateUrl: './main-game.component.html',
    styleUrls: ['./main-game.component.scss']
})
export class GameMainComponent implements OnInit, OnDestroy {

    gameId: number;
    private sub: any;

    constructor(private route: ActivatedRoute) { }

    ngOnInit() {

        this.sub = this.route.params
            .subscribe(params => {
                this.gameId = +params['id'];
            });

        console.log(`Game with id ${this.gameId} initializing.`);
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}
