﻿import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'admin',
    templateUrl: './admin.component.html',
    styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

    ngOnInit() {
        console.log('admin.component initialized');
    }
}
