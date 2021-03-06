﻿import { isDevMode } from '@angular/core';

import { DevConfig, ProdConfig } from './app.config';
import { IConfig } from './app.iconfig';
import { SignalRConfiguration, ConnectionTransports } from 'ng2-signalr';

export function CreateSignalRConfig() {

    let signalrConfiguration: SignalRConfiguration = new SignalRConfiguration();
    signalrConfiguration.hubName = "broadcaster";

    var cfg: IConfig;
    if (isDevMode()) {
        cfg = new DevConfig();
        signalrConfiguration.logging = true;
    }
    else {
        cfg = new ProdConfig();
        signalrConfiguration.logging = false;
    }
    signalrConfiguration.transport = [ConnectionTransports.webSockets, ConnectionTransports.longPolling];

    signalrConfiguration.url = cfg.API_URL + 'signalr';
    
    return signalrConfiguration;
}