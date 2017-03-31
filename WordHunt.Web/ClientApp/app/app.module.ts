import { NgModule } from '@angular/core';

import { CoreModule } from './core/core.module';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { MainModule } from './main/main.module';
import { GameModule } from './game/game.module';

@NgModule({
    imports: [
        CoreModule,
        AppRoutingModule,
        MainModule,
        GameModule,
    ],
    declarations: [
        AppComponent
    ],
    bootstrap: [AppComponent],
})
export class AppModule {
}
