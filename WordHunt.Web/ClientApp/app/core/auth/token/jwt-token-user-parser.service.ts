import { Injectable } from '@angular/core';
import { UserModel } from '../../user.model';
import { JwtHelper } from 'angular2-jwt';

@Injectable()
export class JwtTokenUserParser {
    private jwtHelpter: JwtHelper;

    constructor() {
        this.jwtHelpter = new JwtHelper();
    }

    getUserFromToken(token: string) {
        let tokenData = this.jwtHelpter.decodeToken(token);
        let isExpired = this.jwtHelpter.isTokenExpired(token);
        let user = new UserModel();
        user.email = tokenData.email;
        user.name = tokenData.sub;
        user.loggedIn = !isExpired;
        user.tokenExpirationDate = this.jwtHelpter.getTokenExpirationDate(token);
        user.admin = tokenData.isAdmin == 'true';
        user.id = tokenData.id;

        return user;
    }
}