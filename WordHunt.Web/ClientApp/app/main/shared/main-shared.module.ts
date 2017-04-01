import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


import { MainBackButtonComponent } from './main-back-button/main-back-button.component';

@NgModule({
    imports: [
        RouterModule,
        FormsModule,
        CommonModule
    ],
    exports: [
        MainBackButtonComponent,
        FormsModule,
        CommonModule
    ],
    declarations: [
        MainBackButtonComponent
    ]
})
export class MainSharedModule {
}
