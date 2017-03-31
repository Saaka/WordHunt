import { NgModule } from '@angular/core';

import { MainRoutingModule, routableComponents } from './main.routing.module';

@NgModule({
    imports: [MainRoutingModule],
    declarations: [
        routableComponents
    ]
})
export class MainModule {
}
