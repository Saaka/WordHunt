import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class GameNavigation {

    constructor(private router: Router) { }

    goToGame(gameId: number) {
        
        this.router.navigate(['/game', gameId]);
    }
}