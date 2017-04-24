import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { ConfigService } from '../../config/app.config.service';
import { TokenStorageService } from '../auth/token/token-auth';

@Injectable()
export class AuthHttpService {

    constructor(private http: Http,
        private tokenStorage: TokenStorageService,
        private config: ConfigService) { }

    private createAuthorizationHeader() {
        return this.tokenStorage
            .loadToken()
            .map(response => {
                let headers = new Headers();
                headers.append('Authorization', 'Bearer ' + response);
                return headers;
            });
    }

    get(url) {
        return this.createAuthorizationHeader()
            .mergeMap(response => {
                return this.http.get(this.config.ApiUrl + url, {
                    headers: <Headers>response
                });
            })
            .map(response => {
                return response;
            });
    }

    post(url, data) {
        return this.createAuthorizationHeader()
            .mergeMap(response => {
                return this.http.post(this.config.ApiUrl + url, data, {
                    headers: <Headers>response
                });
            })
            .map(response => {
                return response;
            });
    }
}