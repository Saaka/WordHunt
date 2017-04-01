import { NgModule } from '@angular/core';

import { CoreModule } from './core/core.module';
import { AppComponent } from './app.component';
import { AppRoutingModule, routableComponents } from './app.routing.module';
import { GameModule } from './game/game.module';
import { MainSharedModule } from './main/shared/mainShared.module';

@NgModule({
    imports: [
        CoreModule,
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
