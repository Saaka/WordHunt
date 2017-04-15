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

    validateLoginState(forceReload: boolean = false) {

        if (forceReload || !this.isLoaded) {
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
                    this.isLoaded = true;
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