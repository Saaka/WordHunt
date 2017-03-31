﻿import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { GameComponent } from './game.component';
import { GameMainComponent } from './main/mainGame.component';

const routes: Routes = [
    {
        path: '',
        component: GameComponent,
        children: [
            { path: '', component: GameMainComponent }
        ]
    },    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class GameRoutingModule { }

export const routableComponents = [
    GameComponent, GameMainComponent
];