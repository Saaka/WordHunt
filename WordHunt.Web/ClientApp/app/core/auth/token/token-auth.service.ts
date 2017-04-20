import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { ConfigService } from '../../../config/app.config.service';
import { TokenResponse } from './token-response.model';
import { Observable } from 'rxjs/Observable';
import { RequestOptionsArgs } from '@angular/http';

@Injectable()
export class TokenAuthService {

    constructor(private http: Http,
        private config: ConfigService) { }

    getToken(userName: string, password: string): Observable<TokenResponse> {

        return this.http
            .post(this.config.ApiUrl + 'auth/token', this.createBody(userName, password))
            .map((response: Response) => {
                let tokenResponse = <TokenResponse>response.json();

                return tokenResponse;
            })
            .catch(this.handleError);
    }

    private createBody(userName: string, password: string) {
        return {
            "userName": userName,
            "password": password
        };
    }

    private handleError(error: Response) {
        let msg = `Error status code ${error.status} at ${error.url}`;
        return Observable.throw(msg);
    }
}