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

    createAuthorizationHeader(headers: Headers) {
        let token = this.tokenStorage.loadToken();

        headers.append('Authorization', 'Bearer ' + token);
    }

    get(url) {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.get(this.config.ApiUrl + url, {
            headers: headers
        });
    }

    post(url, data) {
        let headers = new Headers();
        this.createAuthorizationHeader(headers);
        return this.http.post(this.config.ApiUrl + url, data, {
            headers: headers
        });
    }
}