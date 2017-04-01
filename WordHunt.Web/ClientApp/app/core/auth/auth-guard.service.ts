import { Injectable } from '@angular/core';
import {
    Router,
    CanActivate,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
} from '@angular/router';
import { UserService } from '../user.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private router: Router, private userService: UserService) { }

    canActivate(next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot) {

        if (this.userService.isLoggedIn) {
            return true;
        }

        this.router.navigate(['/login'], { queryParams: { redirectTo: state.url } });

        return false;
    }
}