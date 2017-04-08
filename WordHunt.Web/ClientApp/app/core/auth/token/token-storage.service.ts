import { Injectable } from '@angular/core';
import { AsyncLocalStorage } from 'angular-async-local-storage';

@Injectable()
export class TokenStorageService {
    private tokenStorageName = 'whAuthToken';

    constructor(private storage: AsyncLocalStorage) { }

    saveToken(token: string) {
        localStorage.setItem(this.tokenStorageName, token);
    }

    getToken() {
        return localStorage.getItem(this.tokenStorageName);
    }

    deleteToken() {
        localStorage.removeItem(this.tokenStorageName);
    }
}