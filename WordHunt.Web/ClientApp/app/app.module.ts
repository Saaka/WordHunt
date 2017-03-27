import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component';
import { GameMainComponent } from './components/game/main/game.main.component';
import { GameBoardComponent } from './components/game/board/game.board.component';
import { GameSidenavComponent } from './components/game/sidenav/game.sidenav.component';
import { MainMenuComponent } from './components/mainMenu/mainMenu.component';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        GameMainComponent,
        GameBoardComponent,
        GameSidenavComponent,
        MainMenuComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        RouterModule.forRoot([
            { path: '', redirectTo: 'mainmenu', pathMatch: 'full' },
            { path: 'hunt', component: GameMainComponent },
            { path: 'mainmenu', component: MainMenuComponent }
        ])
    ]
})
export class AppModule {
}
