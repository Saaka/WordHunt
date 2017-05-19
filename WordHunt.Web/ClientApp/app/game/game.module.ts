import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GameRoutingModule, routableComponents } from './game.routing.module';
import { GameBoardComponent, GameFieldComponent } from './board/board-components';
import { GameSidenavComponent } from './sidenav/game-sidenav.component';
import { GameNavigationComponent } from './navigation/game-navigation.component';
import { GameHubService, GameService } from './services/game-services.imports';

@NgModule({
    imports: [GameRoutingModule, CommonModule],
    declarations: [
        GameBoardComponent,
        GameSidenavComponent,
        GameNavigationComponent,
        routableComponents,
        GameFieldComponent
    ],
    providers: [GameHubService,
        GameService]
})
export class GameModule {
    
}
