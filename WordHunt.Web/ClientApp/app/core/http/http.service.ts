import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { ConfigService } from '../../config/app.config.service';
import { TokenStorageService } from '../auth/token/token-auth';
import { GuidService } from '../guid.service';

@Injectable()
export class HttpService {

    constructor(private http: Http,
        private config: ConfigService,
        private guidService: GuidService) { }

    private createHeaders() {

        let headers = new Headers();
        headers.append('x-requester-id', this.guidService.create());

        return headers;
    }

    get(url) {
        var headers = this.createHeaders();
        return this.http.get(this.config.ApiUrl + url, {
            headers: headers
        }).catch(err => Observable.throw(err.json()));
    }

    post(url, data) {
        var headers = this.createHeaders();
        return this.http.post(this.config.ApiUrl + url, data, {
            headers: headers
        }).catch(err => Observable.throw(err.json()));
    }
}