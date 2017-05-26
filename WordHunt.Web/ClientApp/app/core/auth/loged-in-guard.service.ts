import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { UserService } from '../user.service';

@Injectable()
export class LoggedInGuard implements CanActivate {

    constructor(private router: Router, private userService: UserService) { }

    canActivate() {

        return this.userService.validateLoginState()
            .map(res => {

                if (!this.userService.isLoggedIn()) {
                    return true;
                }

                this.router.navigate(['/main']);

                return false;
            });
    }
}