import { Component, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

import { LoginService } from '../../core/auth/login.service';
import { UserService } from '../../core/user.service';
import { LoginModel } from './login.model';
import { SnackbarService } from '../../core/core.imports';

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

    constructor(private router: Router,
        private route: ActivatedRoute,
        private loginService: LoginService,
        private userService: UserService,
        private snackbar: SnackbarService) { }

    login() {
        this.loading = true;

        this.loginSub = this.loginService
            .login(this.model.userName, this.model.password)
            .catch((error) => {
                this.loading = false;

                this.snackbar.openSnackbar(error);

                return '';
            })
            .mergeMap(loginResult => {
                return this.route.queryParams;
            })
            .map(qp => qp['redirectTo'])
            .subscribe(redirectTo => {
                this.loading = false;

                if (this.userService.isLoggedIn()) {
                    this.snackbar.openSnackbar(`Logged in. Welcome ${this.userService.userName()}!`)

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
