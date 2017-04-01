import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { MainBackButtonComponent } from './mainBackButton/mainBackButton.component';

@NgModule({
    imports: [
        RouterModule
    ],
    exports: [
        MainBackButtonComponent
    ],
    declarations: [
        MainBackButtonComponent
    ]
})
export class MainSharedModule {
}
