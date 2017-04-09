import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { UserService } from '../user.service';
import { TokenAuthService, TokenResponse, TokenStorageService } from './token/token-auth';

@Injectable()
export class LoginService {

    constructor(private userService: UserService,
        private tokenAuth: TokenAuthService,
        private tokenStorage: TokenStorageService) { }

    login(email: string, password: string) {
        return this.tokenAuth
            .getToken(email, password)
            .mergeMap(response => {
                return this.tokenStorage
                    .saveToken(response.token);
            })
            .map(response => {
                if (response)
                    this.userService.validateLoginState();
            })
            .catch(this.handleError);
    }

    private handleError(error) {
        console.log(error);
        return Observable.throw('Login failed');
    }

    logout() {
        this.tokenStorage.deleteToken();
        this.userService.validateLoginState();
    }
}