import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { TokenAuthService, TokenStorageService } from './auth/token/token-auth';
import { AuthHttpService } from './http/auth-http.service';

@NgModule({
    imports: [HttpModule],
    providers: [
        TokenAuthService,
        TokenStorageService,
        AuthHttpService
    ]
})
export class CoreModule {

}
