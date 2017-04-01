import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule, routableComponents } from './login.routing.module';
import { MainSharedModule } from '../shared/main-shared.module';

@NgModule({
    imports: [
        LoginRoutingModule,
        MainSharedModule,
        FormsModule,
        CommonModule
    ],
    exports: [
        FormsModule,
        CommonModule
    ],
    declarations: [
        routableComponents
    ]
})
export class LoginModule {

}
