import { Component } from '@angular/core';
import { ViewEncapsulation } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-comp',
    encapsulation: ViewEncapsulation.None,
    templateUrl: './app.component.html',
    styleUrls: ['./styles/wordhunt.scss','./app.component.scss']
})
export class AppComponent {
    constructor(private title: Title) {
        this.title.setTitle("WordHunt!");
    }
}
