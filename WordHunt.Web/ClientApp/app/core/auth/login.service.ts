import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { UserService } from '../user.service';

import { ConfigService } from '../../config/app.config.service';

@Injectable()
export class LoginService {

    constructor(private userService: UserService,
        private config: ConfigService) { }

    login(username:string, password:string) {

        console.log(`Username: ${username} Password: ${password}`);
        return Observable.of(true).delay(1000).do(function () { });
    }

    logout() {

    }
}