import { Component, OnInit } from '@angular/core';

import { AuthHttpService } from '../../core/http/auth-http.service';

@Component({
    selector: 'settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {

    constructor(private http: AuthHttpService) { }

    ngOnInit() {
        //http
    }
}
