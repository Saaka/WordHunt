import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { TokenAuthService, TokenStorageService, JwtTokenUserParser } from './auth/token/token-auth';
import { AuthHttpService } from './http/auth-http.service';

@NgModule({
    imports: [HttpModule],
    providers: [
        TokenAuthService,
        TokenStorageService,
        AuthHttpService, 
        JwtTokenUserParser
    ]
})
export class CoreModule {

}
