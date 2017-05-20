import { NgModule } from "@angular/core";
import { MaterialModule } from "@angular/material";

@NgModule({
    imports: [MaterialModule],
    exports: [MaterialModule],
})
export class CustomMaterialModule { }