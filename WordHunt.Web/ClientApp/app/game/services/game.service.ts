import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { HttpService } from '../../core/http/http.service';
import { Game, Team, Field } from '../game.models';

@Injectable()
export class GameService {

    constructor(private http: HttpService) { }

    getGame(gameId: number) {

        return this.http
            .get('game/' + gameId)
            .map(res => res.json());
    }
}