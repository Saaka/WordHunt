import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component';
import { GameMainComponent } from './components/game/main/game.main.component';
import { GameBoardComponent } from './components/game/board/game.board.component';
import { GameSidenavComponent } from './components/game/sidenav/game.sidenav.component';
import { MaterialModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        GameMainComponent,
        GameBoardComponent,
        GameSidenavComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        MaterialModule,
        FlexLayoutModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'hunt', pathMatch: 'full' },
            { path: '**', redirectTo: 'hunt' },
            { path:'hunt', component: GameMainComponent }
        ])
    ]
})
export class AppModule {
}
