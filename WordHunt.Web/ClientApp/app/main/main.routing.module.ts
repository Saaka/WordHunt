import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MainComponent } from './main.component';
import { MainMenuComponent } from './mainMenu/mainMenu.component';
import { TutorialComponent } from './tutorial/tutorial.component';
import { SettingsComponent } from './settings/settings.component';

const routes: Routes = [
    {
        path: 'main',
        component: MainComponent,
        children: [
            {
                path: '',
                component: MainMenuComponent,
            },
            {
                path: 'tutorial',
                component: TutorialComponent,
            },
            {
                path: 'settings',
                component: SettingsComponent,
            },
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MainRoutingModule { }

export const routableComponents = [
    MainComponent, MainMenuComponent, TutorialComponent, SettingsComponent
];