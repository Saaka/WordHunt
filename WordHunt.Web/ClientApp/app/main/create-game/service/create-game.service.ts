import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { AuthHttpService } from '../../../core/http/auth-http.service';
import { GameCreate } from '../game.models';

@Injectable()
export class CreateGameService {

    constructor(private http: AuthHttpService) {

    }

    createGame(model: GameCreate) {

        return this.http
            .post('game', model)
            .subscribe(res => {

                console.log('GAME CREATED');
                console.log(res);
            });            
    }
}