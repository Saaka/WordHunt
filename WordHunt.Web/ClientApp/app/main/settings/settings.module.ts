import { NgModule } from '@angular/core';

import { SettingsRoutingModule, routableComponents } from './settings.routing.module';
import { MainSharedModule } from '../shared/main-shared.module';

@NgModule({
    imports: [
        SettingsRoutingModule,
        MainSharedModule
    ],
    declarations: [
        routableComponents
    ]
})
export class SettingsModule {

}
