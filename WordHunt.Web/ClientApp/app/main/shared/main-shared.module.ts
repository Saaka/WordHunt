import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';


import { MainBackButtonComponent } from './main-back-button/main-back-button.component';

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
