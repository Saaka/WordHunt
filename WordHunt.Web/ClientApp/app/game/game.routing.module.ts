import { NgModule } from '@angular/core';
import { PreloadAllModules, Routes, RouterModule } from '@angular/router';

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
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class GaneRoutingModule { }

export const routableComponents = [
    GameComponent, GameMainComponent
];