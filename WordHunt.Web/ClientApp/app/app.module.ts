import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRouteModule, routableComponents } from './app-routing.module';

import { CoreModule } from './core/core.module';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        routableComponents
    ],
    imports: [
        CoreModule,
        AppRouteModule
    ]
})
export class AppModule {
}
