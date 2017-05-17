import { ISignalRConnection } from './connection/i.signalr.connection';
import { SignalRConfiguration } from './signalr.configuration';
import { SignalRConnection } from './connection/signalr.connection';
import { NgZone, Injectable } from '@angular/core';
import { IConnectionOptions } from './connection/connection.options';
import { ConnectionTransport } from './connection/connection.transport';

declare var jQuery: any;

@Injectable()
export class SignalR {
    private _configuration: SignalRConfiguration;
    private _zone: NgZone;
    private _jHubConnectionFn: any;

    public constructor(configuration: SignalRConfiguration, zone: NgZone, jHubConnectionFn: Function) {
        this._configuration = configuration;
        this._zone = zone;
        this._jHubConnectionFn = jHubConnectionFn;
    }

    public connect(options?: IConnectionOptions): Promise<ISignalRConnection> {

        let $promise = new Promise<SignalRConnection>((resolve, reject) => {

            let configuration = this.merge(options ? options : {});
            let jTransports = this.convertTransports(configuration.transport);

            try {

                let serializedQs = JSON.stringify(configuration.qs);
                let serializedTransport = JSON.stringify(jTransports);

                if (configuration.logging) {
                    console.log(`Connecting with...`);
                    console.log(`configuration:[url: '${configuration.url}'] ...`);
                    console.log(`configuration:[hubName: '${configuration.hubName}'] ...`);
                    console.log(`configuration:[qs: '${serializedQs}'] ...`);
                    console.log(`configuration:[transport: '${serializedTransport}'] ...`);
                }
            } catch (err) {}

            // create connection object
            let jConnection = this._jHubConnectionFn(configuration.url);
            jConnection.logging = configuration.logging;
            jConnection.qs = configuration.qs;

            // create a proxy
            let jProxy = jConnection.createHubProxy(configuration.hubName);
            // !!! important. We need to register at least one on function otherwise server callbacks will not work.
            jProxy.on('noOp', function () { });

            let hubConnection = new SignalRConnection(jConnection, jProxy, this._zone);
            // start the connection
            console.log('Starting SignalR connection ...');

            jConnection.start({ withCredentials: configuration.withCredentials, jsonp: configuration.jsonp, transport: jTransports })
                .done(() => {
                    console.log('Connection established, ID: ' + jConnection.id);
                    console.log('Connection established, Transport: ' + jConnection.transport.name);
                    resolve(hubConnection);
                })
                .fail((error: any) => {
                    console.log('Could not connect');
                    reject('Failed to connect. Error: ' + error.message); // ex: Error during negotiation request.
                });
        });

        return $promise;
    }

    convertTransports(transports: ConnectionTransport | ConnectionTransport[]) : any {
        if(transports instanceof Array)
            return transports.map((t: ConnectionTransport) => t.name);
        
        return transports.name;
    }

    private merge(overrides: IConnectionOptions): SignalRConfiguration {
        let merged: SignalRConfiguration = new SignalRConfiguration();
        merged.hubName = overrides.hubName || this._configuration.hubName;
        merged.url = overrides.url || this._configuration.url;
        merged.qs = overrides.qs || this._configuration.qs;
        merged.logging = this._configuration.logging;
        merged.jsonp = overrides.jsonp || this._configuration.jsonp;
        merged.withCredentials = overrides.withCredentials || this._configuration.withCredentials;
        merged.transport = overrides.transport || this._configuration.transport;
        return merged;
    }

}
