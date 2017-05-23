import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'main',
    templateUrl: './main.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

    ngOnInit() {
        console.log('main.component OnInit');
    }
}
