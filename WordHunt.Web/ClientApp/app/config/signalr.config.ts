import { isDevMode } from '@angular/core';

import { DevConfig, ProdConfig } from './app.config';
import { IConfig } from './app.iconfig';
import { SignalRConfiguration } from 'ng2-signalr';

export function CreateSignalRConfig() {

    var cfg: IConfig;
    if (isDevMode())
        cfg = new DevConfig();
    else
        cfg = new ProdConfig();

    let signalrConfiguration: SignalRConfiguration = new SignalRConfiguration();

    signalrConfiguration.hubName = "broadcaster";
    signalrConfiguration.logging = true;
    signalrConfiguration.url = cfg.API_URL + 'signalr';


    return signalrConfiguration;
}