import { NgModule } from '@angular/core';
import { PreloadAllModules, Routes, RouterModule } from '@angular/router';

import { MainMenuComponent } from './mainMenu/mainMenu.component';

const routes: Routes = [
    { path: '', redirectTo: 'mainmenu', pathMatch: 'full' },
    { path: 'mainmenu', component: MainMenuComponent },
    { path: 'game', loadChildren: './game/game.module#GameModule' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
    exports: [RouterModule]
})
export class AppRoutingModule { }

export const routableComponents = [
    MainMenuComponent
];