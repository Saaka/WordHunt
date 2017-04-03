import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule, routableComponents } from './app.routing.module';
import { GameModule } from './game/game.module';
import './core/rxjs.imports';

@NgModule({
    imports: [
        BrowserModule,
        AppRoutingModule,
        GameModule
    ],
    declarations: [
        AppComponent,
        routableComponents
    ],
    bootstrap: [AppComponent],
})
export class AppModule {
}
