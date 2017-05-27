import { Injectable } from "@angular/core";
import { MdSnackBar, MdSnackBarConfig } from "@angular/material";

@Injectable()
export class SnackbarService {

    private baseConfig: MdSnackBarConfig = {
        duration: 2000
    };
    private baseAction: string = "Ok";
    constructor(private snackbar: MdSnackBar) { }

    openSnackbar(message: string, action?: string, config?: MdSnackBarConfig) {
        this.snackbar.open(message, action || this.baseAction, config || this.baseConfig);
    }
}
