﻿import { Component } from '@angular/core';

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
        return this.userService.isLoggedIn;
    }

    logout() {
        this.loginService.logout();
        console.log('Logged out');
    }
}
