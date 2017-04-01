import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { MainComponent } from './main/main.component';
import { MainMenuComponent } from './main/main-menu/main-menu.component';

import { AuthGuard } from './core/auth/auth-guard.service';
import { UserService } from './core/user.service';

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
                redirectTo: ''
            },
            {
                path: 'tutorial',
                loadChildren: './main/tutorial/tutorial.module#TutorialModule',
            },
            {
                path: 'settings',
                loadChildren: './main/settings/settings.module#SettingsModule',
                canActivate: [AuthGuard]
            },
            {
                path: 'login',
                loadChildren: './main/login/login.module#LoginModule'
            },
        ]
    },
    {
        path: 'game',
        loadChildren: './game/game.module#GameModule'
    },
    {
        path: '**',
        redirectTo: 'main',
        pathMatch: 'full'
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
    exports: [RouterModule],
    providers: [
        AuthGuard,
        UserService
    ]
})
export class AppRoutingModule { }

export const routableComponents = [
    MainComponent, MainMenuComponent
];