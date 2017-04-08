import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { UserService } from '../user.service';
import { TokenAuthService, TokenResponse } from './token/token-auth';

@Injectable()
export class LoginService {
    private tokenStorageName = 'whAuthToken';

    constructor(private userService: UserService,
        private tokenAuth: TokenAuthService) { }

    login(email: string, password: string) {
        return this.tokenAuth
            .getToken(email, password)
            .map(response => {
                localStorage.setItem(this.tokenStorageName, response.token);

                this.userService.validateLoginState();
            })
            .catch(this.handleError);

    }

    private handleError(error) {
        return Observable.throw('Login failed');
    }

    logout() {
        localStorage.removeItem(this.tokenStorageName);
        this.userService.validateLoginState();
    }
}