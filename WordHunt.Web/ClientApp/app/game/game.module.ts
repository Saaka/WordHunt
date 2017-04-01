import { NgModule } from '@angular/core';

import { GameRoutingModule, routableComponents } from './game.routing.module';
import { GameBoardComponent } from './board/gameBoard.component';
import { GameSidenavComponent } from './sidenav/gameSidenav.component';

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
