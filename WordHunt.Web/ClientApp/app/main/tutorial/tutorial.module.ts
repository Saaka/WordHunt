import { NgModule } from '@angular/core';

import { TutorialRoutingModule, routableComponents } from './tutorial.routing.module';
import { MainSharedModule } from '../shared/main-shared.module';

@NgModule({
    imports: [
        TutorialRoutingModule,
        MainSharedModule
    ],
    declarations: [
        routableComponents
    ]
})
export class TutorialModule {

}
