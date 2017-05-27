import { Component } from '@angular/core';

import { LoginService } from '../../core/auth/login.service';
import { UserService } from '../../core/user.service';
import { SnackbarService } from '../../core/core.imports';

@Component({
    selector: 'main-menu',
    templateUrl: './main-menu.component.html',
    styleUrls: ['./main-menu.component.scss'],
    providers: [LoginService]
})
export class MainMenuComponent {

    constructor(private loginService: LoginService,
        private userService: UserService,
        private snackbar: SnackbarService) { }

    isLoggedIn() {
        return this.userService.isLoggedIn();
    }

    isAdmin() {
        return this.userService.isAdmin();
    }

    getUserName() {
        return this.userService.userName();
    }

    logout() {
        this.loginService
            .logout()
            .subscribe(response => {
                this.snackbar.openSnackbar('Logged out');
            });
    }
}
