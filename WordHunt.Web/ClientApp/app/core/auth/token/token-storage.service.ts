import { Injectable } from '@angular/core';

@Injectable()
export class TokenStorageService {
    private tokenStorageName = 'whAuthToken';

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