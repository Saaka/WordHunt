import { Component } from '@angular/core';

@Component({
    selector: 'main-menu',
    templateUrl: './main-menu.component.html',
    styleUrls: ['./main-menu.component.scss']
})
export class MainMenuComponent {

    logout() {
        console.log('LOGGED OUT');
    }
}
