import { NgModule } from '@angular/core';
import { PreloadAllModules, Routes, RouterModule } from '@angular/router';
import { MainMenuComponent } from './mainMenu/mainMenu.component';

const routes: Routes = [
    { path: '', redirectTo: 'mainmenu', pathMatch: 'full' },
    { path: 'mainmenu', component: MainMenuComponent },
    { path: 'game', loadChildren: 'ClientApp/app/game/game.module#GameComponent' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
    exports: [RouterModule]
})
export class AppRouteModule { }

export const routableComponents = [
    MainMenuComponent
];