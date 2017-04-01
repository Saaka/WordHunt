import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent {
    model: any = {};
    loading = false;
    error = '';

    constructor(private router: Router) { }

    login() {
        this.loading = true;
        console.log('log in eventually');
    }
}
