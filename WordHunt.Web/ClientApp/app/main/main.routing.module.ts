import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MainComponent } from './main.component';
import { MainMenuComponent } from './mainMenu/mainMenu.component';

const routes: Routes = [
    {
        path: 'mainmenu',
        component: MainComponent,
        //outlet: 'main-outlet',
        children: [
            { path: '', component: MainMenuComponent, outlet: 'main-menu-outlet' }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MainRoutingModule { }

export const routableComponents = [
    MainComponent, MainMenuComponent
];