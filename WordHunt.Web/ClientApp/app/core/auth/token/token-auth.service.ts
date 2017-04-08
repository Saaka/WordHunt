import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { ConfigService } from '../../../config/app.config.service';
import { TokenResponse } from './token-response.model';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class TokenAuthService {

    constructor(private http: Http,
        private config: ConfigService) { }

    getToken(email: string, password: string): Observable<TokenResponse> {

        return this.http
            .post(this.config.ApiUrl + 'auth/token', this.createBody(email, password))
            .map((response: Response) => {
                let tokenResponse = <TokenResponse>response.json();

                return tokenResponse;
            })
            .catch(this.handleError);
    }

    private createBody(email: string, password: string) {
        return {
            "email": email,
            "password": password
        };
    }

    private handleError(error: Response) {
        console.log(error);
        let msg = `Error status code ${error.status} at ${error.url}`;
        return Observable.throw(msg);
    }
}