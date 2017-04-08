import { Injectable } from '@angular/core';

//import { TokenStorageService } from './auth/token/token-auth';

@Injectable()
export class UserService {

    private loggedIn: boolean = false;

    constructor() {
        this.validateLoginState();
    }

    isLoggedIn() {
        return this.loggedIn;
    }

    validateLoginState() {

        console.log('login state validated');
        this.loggedIn = !this.loggedIn;
    }
}