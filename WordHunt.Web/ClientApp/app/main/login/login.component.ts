import { Component, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

import { LoginService } from '../../core/auth/login.service';
import { UserService } from '../../core/user.service';
import { LoginModel } from './login.model';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    providers: [LoginService]
})
export class LoginComponent implements OnDestroy {
    private loginSub: Subscription;
    model: LoginModel = new LoginModel();
    loading = false;
    error = '';

    constructor(private router: Router,
        private route: ActivatedRoute,
        private loginService: LoginService,
        private userService: UserService) { }

    login() {
        this.loading = true;

        this.loginSub = this.loginService
            .login(this.model.username, this.model.password)
            .catch((error) => {
                this.loading = false;

                console.log(error);

                return '';
            })
            .mergeMap(loginResult => {
                return this.route.queryParams;
            })
            .map(qp => qp['redirectTo'])
            .subscribe(redirectTo => {
                this.loading = false;

                if (this.userService.isLoggedIn()) {
                    console.log('Logged in');

                    let url = redirectTo ? [redirectTo] : ['/main'];
                    this.router.navigate(url);
                }

            });
    }

    ngOnDestroy() {
        if (this.loginSub) {
            this.loginSub.unsubscribe();
        }
    }
}
