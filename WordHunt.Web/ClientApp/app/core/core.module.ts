import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { TokenAuthService, TokenStorageService, JwtTokenUserParser } from './auth/token/token-auth';
import { AuthHttpService } from './http/auth-http.service';
import { HttpService } from './http/http.service';
import { GameNavigation } from './navigation/navigation.module';
import { GuidService } from './guid.service';

@NgModule({
    imports: [HttpModule],
    providers: [
        TokenAuthService,
        TokenStorageService,
        AuthHttpService,
        HttpService, 
        GuidService,
        JwtTokenUserParser,
        GameNavigation
    ]
})
export class CoreModule {

}
