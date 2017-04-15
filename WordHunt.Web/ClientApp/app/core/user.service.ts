import { Injectable } from '@angular/core';

import { TokenStorageService, JwtTokenUserParser } from './auth/token/token-auth';
import { UserModel } from './user.model';

@Injectable()
export class UserService {

    private activeUser: UserModel;

    constructor(private storage: TokenStorageService,
        private tokenParser: JwtTokenUserParser) {
        this.initData();
        //this.validateLoginState()
        //    .subscribe(response => {
        //        console.log(`User is logged in: ${response}`);
        //    });
    }

    isLoggedIn() {
        return this.activeUser.loggedIn;
    }

    userName() {
        return this.activeUser.name;
    }

    userEmail() {
        return this.activeUser.email;
    }

    isAdmin() {
        return this.activeUser.admin;
    }

    validateLoginState() {
        return this.storage.loadToken()
            .map(response => {

                if (response) {
                    this.activeUser = this.tokenParser.getUserFromToken(response);
                    if (!this.activeUser.loggedIn)
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
        this.activeUser = new UserModel();
        this.activeUser.loggedIn = false;
    }

    loginFailed() {
        this.initData();
    }
}