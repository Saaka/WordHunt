import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { CustomMaterialModule } from '../../core/material/custom-material.module';

@NgModule({
    imports: [
        RouterModule,
        FormsModule,
        CommonModule,
        CustomMaterialModule
    ],
    exports: [
        RouterModule,
        FormsModule,
        CommonModule,
        CustomMaterialModule
    ]
})
export class MainSharedModule {
}
