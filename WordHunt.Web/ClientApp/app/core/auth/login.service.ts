import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { UserService } from '../user.service';
import { TokenAuthService, TokenResponse } from './token/token-auth';

@Injectable()
export class LoginService {
    private tokenStorageName = 'whAuthToken';

    constructor(private userService: UserService,
        private tokenAuth: TokenAuthService) { }

    login(email: string, password: string): Observable<LoginResult> {
        return this.tokenAuth
            .getToken(email, password)
            .map(
            response => {
                localStorage.setItem(this.tokenStorageName, response.token);

                this.userService.validateLoginState();
                return new LoginResult();
            },
            error => {
                let result = new LoginResult();
                result.isError = true;
                result.message = 'Login failed';
                return result;
            })
            .catch(this.handleError);

    }

    private handleError(error: TokenResponse) {
        console.log(error);

        return Observable.throw('');
    }

    logout() {
        localStorage.removeItem(this.tokenStorageName);
        this.userService.validateLoginState();
    }
}

export class LoginResult {
    isError: boolean;
    message: string;
}