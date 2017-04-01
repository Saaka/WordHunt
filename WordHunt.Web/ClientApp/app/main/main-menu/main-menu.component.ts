import { Component } from '@angular/core';

import { LoginService } from '../../core/auth/login.service';

@Component({
    selector: 'main-menu',
    templateUrl: './main-menu.component.html',
    styleUrls: ['./main-menu.component.scss'],
    providers: [LoginService]
})
export class MainMenuComponent {

    constructor(private loginService: LoginService) { }

    logout() {
        this.loginService.logout();
        console.log('Logged out');
    }
}
