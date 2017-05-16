import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { HttpService } from '../../core/http/http.service';
import { AuthHttpService } from '../../core/http/auth-http.service';
import { Game, Team, Field, TeamChanged } from '../game.models';

@Injectable()
export class GameService {

    constructor(private http: HttpService,
        private authHttp: AuthHttpService) { }

    getGame(gameId: number) {

        return this.http
            .get('game/' + gameId)
            .map(res => <Game>res.json());
    }

    skipRound(gameId: number) {

        return this.authHttp
            .get('game/' + gameId + "/skipround");
            //.map(res => <TeamChanged>res.json());
    }
}