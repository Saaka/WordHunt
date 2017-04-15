﻿import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { MainComponent } from './main/main.component';
import { MainMenuComponent } from './main/main-menu/main-menu.component';

import { AuthGuard, AdminAuthGuard, LoggedInGuard } from './core/auth/guards';
import { UserService } from './core/user.service';

import { ConfigService } from './config/app.config.service';

import { StartupGuard } from './core/startup.service';

const routes: Routes = [
    {
        path: '',
        component: MainComponent,
        canActivate: [StartupGuard],
        children: [
            {
                path: '',
                component: MainMenuComponent
            },
            {
                path: 'main',
                redirectTo: ''
            },
            {
                path: 'tutorial',
                loadChildren: './main/tutorial/tutorial.module#TutorialModule',
                canActivate: [AuthGuard]
            },
            {
                path: 'settings',
                loadChildren: './main/settings/settings.module#SettingsModule',
                canActivate: [AuthGuard]
            },
            {
                path: 'admin',
                loadChildren: './main/admin/admin.module#AdminModule',
                canActivate: [AuthGuard, AdminAuthGuard]
            },
            {
                path: 'login',
                loadChildren: './main/login/login.module#LoginModule',
                canActivate: [LoggedInGuard]
            },
        ]
    },
    {
        path: 'game',
        loadChildren: './game/game.module#GameModule',
        canActivate: [AuthGuard]
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
        LoggedInGuard,
        AdminAuthGuard,
        UserService,
        ConfigService,
        StartupGuard
    ]
})
export class AppRoutingModule { }

export const routableComponents = [
    MainComponent, MainMenuComponent
];