import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'main-game',
    templateUrl: './main-game.component.html',
    styleUrls: ['./main-game.component.scss']
})
export class GameMainComponent implements OnInit {

    ngOnInit() {

        console.log('Game initialized');
    }
}
