import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MainComponent } from './main/main.component';
import { MainMenuComponent } from './main/mainMenu/mainMenu.component';
import { TutorialComponent } from './main/tutorial/tutorial.component';
import { SettingsComponent } from './main/settings/settings.component';
import { LoginComponent } from './main/login/login.component';

const routes: Routes = [
    {
        path: '',
        component: MainComponent,
        children: [
            {
                path: '',
                component: MainMenuComponent,
            },
            {
                path: 'main',
                redirectTo: '',
                pathMatch: 'full'
            },
            {
                path: 'tutorial',
                component: TutorialComponent,
            },
            {
                path: 'settings',
                component: SettingsComponent,
            },
            {
                path: 'login',
                component: LoginComponent,
            },
        ]
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }

export const routableComponents = [
    MainComponent, MainMenuComponent, TutorialComponent, SettingsComponent, LoginComponent
];