import { Injectable, isDevMode } from '@angular/core';

import { DevConfig, ProdConfig } from './app.config';
import { IConfig } from './app.iconfig';

@Injectable()
export class ConfigService {
    private cfg: IConfig;
    constructor() {
        if (isDevMode())
            this.cfg = new DevConfig();
        else
            this.cfg = new ProdConfig();
    }

    get ApiUrl(): string {
        return this.cfg.API_URL;
    }
}