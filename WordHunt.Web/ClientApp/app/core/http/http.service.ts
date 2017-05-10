import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { ConfigService } from '../../config/app.config.service';
import { TokenStorageService } from '../auth/token/token-auth';

@Injectable()
export class HttpService {

    constructor(private http: Http,
        private config: ConfigService) { }

    get(url) {
        return this.http.get(this.config.ApiUrl + url)
            .catch(err => Observable.throw(err.json()));
    }

    post(url, data) {
        return this.http.post(this.config.ApiUrl + url, data)
            .catch(err => Observable.throw(err.json()));
    }
}