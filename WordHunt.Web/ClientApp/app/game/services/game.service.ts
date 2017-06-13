import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { HttpService } from '../../core/http/http.service';
import { AuthHttpService } from '../../core/http/auth-http.service';
import { Game, Team, Field, TeamChanged, FieldChecked, GameRestart } from '../game.models';
import { UserService } from '../../core/core.imports';

@Injectable()
export class GameService {

    constructor(private http: HttpService,
        private authHttp: AuthHttpService,
        private userService: UserService) { }

    getGame(gameId: number) {

        return this.http
            .get('game/' + gameId)
            .map(res => <Game>res.json());
    }

    restartGame(gameId: number) {

        return this.authHttp
            .get('game/' + gameId + '/restart')
            .map(res => <GameRestart>res.json());
    }

    getGameMap(gameId: number) {

        return this.http
            .get('game/' + gameId + '/map')
            .map(res => <Game>res.json());
    }

    skipRound(gameId: number) {

        return this.authHttp
            .get('game/' + gameId + "/skipround")
            .map(res => <TeamChanged>res.json());
    }

    checkField(gameId: number, fieldId: number) {

        return this.authHttp
            .get('game/' + gameId + '/field/' + fieldId + '/check')
            .map(res => <FieldChecked>res.json());
    }

    canRestart(game: Game) {
        return game.userId == this.userService.userId();
    }
}