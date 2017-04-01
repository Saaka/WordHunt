import { NgModule } from '@angular/core';

import { LoginRoutingModule, routableComponents } from './login.routing.module';
import { MainSharedModule } from '../shared/main-shared.module';

@NgModule({
    imports: [
        LoginRoutingModule,
        MainSharedModule
    ],
    declarations: [
        routableComponents
    ]
})
export class LoginModule {

}
