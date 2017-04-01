import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { UserService } from '../user.service';

@Injectable()
export class LoginService {

    constructor(private userService: UserService) { }

    login() {
        return Observable.of(true)
            .delay(1000)
            .do(this.toggleLogState.bind(this));
    }

    logout() {
        this.toggleLogState(false);
    }

    private toggleLogState(val: boolean) {
        this.userService.isLoggedIn = val;
    }
}