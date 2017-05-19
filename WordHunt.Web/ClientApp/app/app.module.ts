import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AsyncLocalStorageModule } from 'angular-async-local-storage';
 
import { AppComponent } from './app.component';
import { AppRoutingModule, routableComponents } from './app.routing.module';
import { GameModule } from './game/game.module';
import { CoreModule } from './core/core.module';
import './core/rxjs.imports';

import 'expose-loader?jQuery!jquery';
import 'signalr';
import 'hammerjs';
import { CreateSignalRConfig } from './config/signalr.config';
import { SignalRModule, SignalRConfiguration } from './lib/ng2-signalr';

import { CustomMaterialModule } from './core/material/custom-material.module';

@NgModule({
    imports: [
        BrowserModule,
        CustomMaterialModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        GameModule,
        CoreModule,
        AsyncLocalStorageModule,
        SignalRModule.forRoot(CreateSignalRConfig)
    ],
    declarations: [
        AppComponent,
        routableComponents
    ],
    bootstrap: [AppComponent],
})
export class AppModule {
}
