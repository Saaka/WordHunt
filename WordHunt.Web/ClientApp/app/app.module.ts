import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './app.component';
import { GameMainComponent } from './game/game.component';
import { GameBoardComponent } from './game/board/gameBoard.component';
import { GameSidenavComponent } from './game/sidenav/gameSidenav.component';
import { MainMenuComponent } from './mainMenu/mainMenu.component';

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
        FormsModule,
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
