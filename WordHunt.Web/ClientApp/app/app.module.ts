import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutingModule, routableComponents } from './app.routing.module';

import { CoreModule } from './core/core.module';

@NgModule({
    imports: [
        CoreModule,
        AppRoutingModule
    ],
    declarations: [
        AppComponent,
        routableComponents
    ],
    bootstrap: [AppComponent],
})
export class AppModule {
}
