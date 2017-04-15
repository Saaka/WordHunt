import { Injectable } from '@angular/core';

import { TokenStorageService, JwtTokenUserParser } from './auth/token/token-auth';
import { UserModel } from './user.model';
import { Observable } from 'rxjs';


@Injectable()
export class UserService {

    private activeUser: UserModel;
    private isLoaded: boolean = false;

    constructor(private storage: TokenStorageService,
        private tokenParser: JwtTokenUserParser) {
        this.initData();
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

    validateLoginState(forceReload: boolean = false) {

        if (forceReload || !this.isLoaded) {
            return this.storage.loadToken()
                .map(response => {
                    if (response) {
                        this.isLoaded = true;
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
        else
            return Observable
                .of(true)
                .map(x => {
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