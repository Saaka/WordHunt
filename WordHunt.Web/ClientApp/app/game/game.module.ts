import { NgModule } from '@angular/core';

import { GameRoutingModule, routableComponents } from './game.routing.module';
import { GameBoardComponent } from './board/game-board.component';
import { GameSidenavComponent } from './sidenav/game-sidenav.component';

@NgModule({
    imports: [GameRoutingModule],
    declarations: [
        GameBoardComponent,
        GameSidenavComponent,
        routableComponents
    ]
})
export class GameModule {
    
}
