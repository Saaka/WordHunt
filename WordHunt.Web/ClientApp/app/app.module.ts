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
import { SignalRModule, SignalRConfiguration } from 'ng2-signalr';

import { CustomMaterialModule } from './core/material/custom-material.module';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MdDialogModule } from '@angular/material';
//import { GameEndedDialog } from './game/dialogs/dialog.imports';

@NgModule({
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        CustomMaterialModule,
        FlexLayoutModule,
        AppRoutingModule,
        GameModule,
        CoreModule,
        AsyncLocalStorageModule,
        SignalRModule.forRoot(CreateSignalRConfig), 
        MdDialogModule
    ],
    declarations: [
        AppComponent,
        routableComponents
    ],
    bootstrap: [AppComponent],
})
export class AppModule {
}
