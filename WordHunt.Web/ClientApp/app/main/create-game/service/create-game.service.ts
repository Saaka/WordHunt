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
            .post('game/create', model)
            .map(res => res.json());            
    }
}