import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { ConfigService } from '../../config/app.config.service';
import { TokenStorageService } from '../auth/token/token-auth';
import { GuidService } from '../guid.service';

@Injectable()
export class AuthHttpService {

    constructor(private http: Http,
        private tokenStorage: TokenStorageService,
        private config: ConfigService,
        private guidService: GuidService) { }

    private createHeaders() {

        let headers = new Headers();
        headers.append('x-requester-id', this.guidService.create());

        return this.tokenStorage
            .loadToken()
            .map(response => {
                headers.append('Authorization', 'Bearer ' + response);
                return headers;
            });
    }

    get(url) {
        return this.createHeaders()
            .mergeMap(response => {
                return this.http.get(this.config.ApiUrl + url, {
                    headers: response
                });
            })
            .map(response => {
                return response;
            })
            .catch(err => Observable.throw(err.json()));
    }

    post(url, data) {
        return this.createHeaders()
            .mergeMap(response => {
                return this.http.post(this.config.ApiUrl + url, data, {
                    headers: response
                });
            })
            .map(response => {
                return response;
            })
            .catch(err => Observable.throw(err.json()));
    }
}