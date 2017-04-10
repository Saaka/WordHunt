import { Injectable } from '@angular/core';
import {
    Router,
    CanActivate,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
} from '@angular/router';
import { UserService } from '../user.service';

@Injectable()
export class AdminAuthGuard implements CanActivate {

    constructor(private router: Router, private userService: UserService) { }

    canActivate(next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot) {

        if (this.userService.isAdmin()) {
            return true;
        }
        else {
            this.router.navigate(['/main']);
        }
        return false;
    }
}