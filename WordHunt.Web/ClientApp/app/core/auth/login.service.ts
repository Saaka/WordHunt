﻿import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { UserService } from '../user.service';
import { TokenAuthService, TokenResponse, TokenStorageService } from './token/token-auth';

@Injectable()
export class LoginService {

    constructor(private userService: UserService,
        private tokenAuth: TokenAuthService,
        private tokenStorage: TokenStorageService) { }

    login(userName: string, password: string) {
        return this.tokenAuth
            .getToken(userName, password)
            .mergeMap(response => {
                return this.tokenStorage
                    .saveToken(response.token);
            })
            .mergeMap(response => {
                return this.userService
                    .validateLoginState(true);
            })
            .catch(this.handleError);
    }

    private handleError(error) {
        return Observable.throw('Login failed');
    }

    logout() {
        return this.tokenStorage
            .deleteToken()
            .mergeMap(response => {
                return this.userService.validateLoginState(true);
            });
    }
}