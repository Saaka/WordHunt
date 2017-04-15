import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from './user.service';

@Injectable()
export class ContactResolve implements Resolve<boolean> {

    constructor(private userService: UserService) { }

    resolve(route: ActivatedRouteSnapshot) {

        return this.userService.validateLoginState()
            .map(res => {
                return res;
            });
    }
}