import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'game-main',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.scss']
})
export class GameMainComponent implements OnInit {

    ngOnInit() {

        console.log('Game initialized');
    }
}
