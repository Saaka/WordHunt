import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { GameComponent } from './game.component';
import { GameMainComponent } from './main/main-game.component';
import { GameMapComponent } from './map/game-map.component';

const routes: Routes = [
    {
        path: '',
        component: GameComponent,
        children: [
            { path: '', component: GameMainComponent },
            { path: 'map', component: GameMapComponent }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class GameRoutingModule { }

export const routableComponents = [
    GameComponent, GameMainComponent, GameMapComponent
];