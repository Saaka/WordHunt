import { NgModule } from '@angular/core';

import { AdminRoutingModule, routableComponents } from './admin.routing.module';
import { MainSharedModule } from '../shared/main-shared.module';

@NgModule({
    imports: [
        AdminRoutingModule,
        MainSharedModule
    ],
    declarations: [
        routableComponents
    ]
})
export class AdminModule {

}
