import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { TokenAuthService } from './token/token-auth.service';

@NgModule({
    imports: [HttpModule],
    providers: [
        TokenAuthService
    ]
})
export class AuthModule {

}
