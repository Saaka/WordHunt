﻿import { Injectable } from '@angular/core';
import { AsyncLocalStorage } from 'angular-async-local-storage';

@Injectable()
export class TokenStorageService {
    private tokenStorageName = 'whauthtoken';

    constructor(private storage: AsyncLocalStorage) { }

    saveToken(token: string) {
        //localStorage.setItem(this.tokenStorageName, token);
        return this.storage.setItem(this.tokenStorageName, token);
    }

    loadToken() {
        //return localStorage.getItem(this.tokenStorageName);
        return this.storage
            .getItem(this.tokenStorageName)
            .map(response => {
                return <string>response;
            });
    }

    deleteToken() {
        //localStorage.removeItem(this.tokenStorageName);
        return this.storage.removeItem(this.tokenStorageName);
    }
}