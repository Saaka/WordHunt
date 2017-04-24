import { NgModule } from '@angular/core';

import { CreateGamesRoutingModule, routableComponents } from './create-game.routing.module';
import { MainSharedModule } from '../shared/main-shared.module';

@NgModule({
    imports: [
        CreateGamesRoutingModule,
        MainSharedModule
    ],
    declarations: [
        routableComponents
    ]
})
export class CreateGameModule { }
