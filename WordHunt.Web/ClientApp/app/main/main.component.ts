import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'main',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
    
    ngOnInit() {
        console.log('*** main.component *** INITIALIZED');
    }
}
