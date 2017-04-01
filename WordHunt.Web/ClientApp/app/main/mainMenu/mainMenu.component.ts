import { Component } from '@angular/core';

@Component({
    selector: 'main-menu',
    templateUrl: './mainMenu.component.html',
    styleUrls: ['./mainMenu.component.scss']
})
export class MainMenuComponent {

    logout() {
        console.log('LOGGED OUT');
    }
}
