import { Component, Input } from '@angular/core';

@Component({
    selector: 'game-board',
    templateUrl: './game-board.component.html',
    styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent {

    @Input() gameId: number;

}
