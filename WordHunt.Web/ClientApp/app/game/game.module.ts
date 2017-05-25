import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';

import { GameRoutingModule, routableComponents } from './game.routing.module';
import { GameBoardComponent, GameFieldComponent } from './board/board-components';
import { GameSidenavComponent } from './sidenav/game-sidenav.component';
import { GameNavigationComponent } from './navigation/game-navigation.component';
import { GameHubService, GameService } from './services/game-services.imports';
import { CustomMaterialModule } from '../core/material/custom-material.module';

@NgModule({
    imports: [
        FlexLayoutModule,
        GameRoutingModule,
        CommonModule,
        CustomMaterialModule
    ],
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
