import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CreateGameComponent } from './create-game.component';
import { CreateGameService } from './service/create-game.service';

const routes: Routes = [
    {
        path: '',
        component: CreateGameComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
    providers: [CreateGameService]
})
export class CreateGamesRoutingModule { }

export const routableComponents = [
    CreateGameComponent
];