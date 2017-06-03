import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';

import { GameRoutingModule, routableComponents } from './game.routing.module';
import { GameBoardComponent, GameFieldComponent } from './board/board-components';
import { MapBoardComponent, MapFieldComponent } from './map-board/map-board-components';
import { GameSidenavComponent } from './sidenav/game-sidenav.component';
import { GameNavigationComponent } from './navigation/game-navigation.component';
import { GameHubService, GameService, GameDialogsService } from './services/game-services.imports';
import { CustomMaterialModule } from '../core/material/custom-material.module';
import { GameEndedDialog } from './dialogs/dialog.imports';

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
        GameFieldComponent,
        MapBoardComponent,
        MapFieldComponent,
        GameEndedDialog
    ],
    entryComponents: [
        GameEndedDialog
    ],
    providers: [GameHubService,
        GameService,
        GameDialogsService
    ]
})
export class GameModule {
    
}
