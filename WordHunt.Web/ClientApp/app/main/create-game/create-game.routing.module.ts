import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CreateGameComponent } from './create-game.component';

const routes: Routes = [
    {
        path: '',
        component: CreateGameComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CreateGamesRoutingModule { }

export const routableComponents = [
    CreateGameComponent
];