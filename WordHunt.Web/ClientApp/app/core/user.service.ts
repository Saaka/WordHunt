import { Injectable } from '@angular/core';

@Injectable()
export class UserService {

    private loggedIn: boolean = false;

    isLoggedIn() {
        return this.loggedIn;
    }

    validateLoginState() {
        this.loggedIn = !this.loggedIn;
    }
}