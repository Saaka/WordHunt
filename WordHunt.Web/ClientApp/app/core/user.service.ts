import { Injectable } from '@angular/core';

import { TokenStorageService } from './auth/token/token-auth';

@Injectable()
export class UserService {

    private loggedIn: boolean = false;

    constructor(private storage: TokenStorageService) {
        this.validateLoginState()
            .subscribe(response => {
                console.log(`User is logged in: ${this.loggedIn}`);
            });
    }

    isLoggedIn() {
        return this.loggedIn;
    }

    validateLoginState() {
        return this.storage.loadToken()
            .map(response => {
                if (response)
                    this.loggedIn = true;
                else
                    this.loggedIn = false;
                console.log('login state validated');
                return this.loggedIn;
            });
    }
}