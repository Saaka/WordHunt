import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'main-game',
    templateUrl: './mainGame.component.html',
    styleUrls: ['./mainGame.component.scss']
})
export class GameMainComponent implements OnInit {

    ngOnInit() {

        console.log('Game initialized');
    }
}
