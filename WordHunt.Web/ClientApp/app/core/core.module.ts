import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { TokenAuthService, TokenStorageService, JwtTokenUserParser } from './auth/token/token-auth';
import { AuthHttpService } from './http/auth-http.service';
import { GameNavigation } from './navigation/navigation.module';


@NgModule({
    imports: [HttpModule],
    providers: [
        TokenAuthService,
        TokenStorageService,
        AuthHttpService, 
        JwtTokenUserParser,
        GameNavigation
    ]
})
export class CoreModule {

}
