import { Component, OnInit } from '@angular/core';

@Component({
    templateUrl: './word.component.html',
    styleUrls: ['./word.component.scss']
})
export class WordsComponent implements OnInit {

    ngOnInit() {
        console.log('*** word.component *** INITIALIZED');
    }
}
