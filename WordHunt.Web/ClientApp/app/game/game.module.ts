﻿import { NgModule } from '@angular/core';

import { GameRoutingModule, routableComponents } from './game.routing.module';
import { GameBoardComponent } from './board/game-board.component';
import { GameSidenavComponent } from './sidenav/game-sidenav.component';
import { GameHubService, GameService } from './services/game-services.imports';

@NgModule({
    imports: [GameRoutingModule],
    declarations: [
        GameBoardComponent,
        GameSidenavComponent,
        routableComponents
    ],
    providers: [GameHubService,
        GameService]
})
export class GameModule {
    
}
