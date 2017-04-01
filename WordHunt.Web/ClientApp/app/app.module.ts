import { NgModule } from '@angular/core';
import { UniversalModule } from 'angular2-universal';

import { AppComponent } from './app.component';
import { AppRoutingModule, routableComponents } from './app.routing.module';
import { GameModule } from './game/game.module';
import { MainSharedModule } from './main/shared/main-shared.module';
import './core/rxjs.imports';

@NgModule({
    imports: [
        UniversalModule,
        AppRoutingModule,
        GameModule,
        MainSharedModule
    ],
    declarations: [
        AppComponent,
        routableComponents
    ],
    bootstrap: [AppComponent],
})
export class AppModule {
}
