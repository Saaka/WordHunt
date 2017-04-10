import { Injectable } from '@angular/core';

import { TokenStorageService, JwtTokenUserParser } from './auth/token/token-auth';
import { UserModel } from './user.model';

@Injectable()
export class UserService {

    private user: UserModel;

    constructor(private storage: TokenStorageService,
        private tokenParser: JwtTokenUserParser) {
        this.initData();
        this.validateLoginState()
            .subscribe(response => {
                console.log(`User is logged in: ${response}`);
            });
    }

    isLoggedIn() {
        return this.user.loggedIn;
    }

    userName() {
        return this.user.name;
    }

    userEmail() {
        return this.user.email;
    }

    isAdmin() {
        return this.user.admin;
    }

    validateLoginState() {
        return this.storage.loadToken()
            .map(response => {

                if (response) {
                    this.user = this.tokenParser.getUserFromToken(response);
                    if (!this.user.loggedIn)
                        this.loginFailed();
                }
                else {
                    this.loginFailed();
                }

                console.log('login state validated');
                return this.isLoggedIn();
            });
    }

    initData() {
        this.user = new UserModel();
        this.user.loggedIn = false;
    }

    loginFailed() {
        this.initData();
    }
}