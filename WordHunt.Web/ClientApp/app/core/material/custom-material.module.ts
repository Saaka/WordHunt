import { NgModule } from "@angular/core";
import { MdButtonModule, MdCheckboxModule, MdSidenavModule } from "@angular/material";

@NgModule({
    imports: [MdButtonModule, MdCheckboxModule, MdSidenavModule],
    exports: [MdButtonModule, MdCheckboxModule, MdSidenavModule],
})
export class CustomMaterialModule { }