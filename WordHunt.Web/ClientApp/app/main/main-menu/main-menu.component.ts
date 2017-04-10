import { Component } from '@angular/core';

import { LoginService } from '../../core/auth/login.service';
import { UserService } from '../../core/user.service';

@Component({
    selector: 'main-menu',
    templateUrl: './main-menu.component.html',
    styleUrls: ['./main-menu.component.scss'],
    providers: [LoginService]
})
export class MainMenuComponent {

    constructor(private loginService: LoginService,
                private userService: UserService) { }

    isLoggedIn() {
        return this.userService.isLoggedIn();
    }

    isAdmin() {
        return this.userService.isAdmin();
    }

    logout() {
        this.loginService
            .logout()
            .subscribe(response => {
                console.log('Logged out');
            });
    }
}
