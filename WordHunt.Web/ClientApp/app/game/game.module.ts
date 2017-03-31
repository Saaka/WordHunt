import { NgModule } from '@angular/core';

import { GaneRoutingModule, routableComponents } from './game.routing.module';
import { GameBoardComponent } from './board/gameBoard.component';
import { GameSidenavComponent } from './sidenav/gameSidenav.component';

@NgModule({
    imports: [GaneRoutingModule],
    declarations: [
        GameBoardComponent,
        GameSidenavComponent,
        routableComponents
    ]
})
export class GameModule {
}
