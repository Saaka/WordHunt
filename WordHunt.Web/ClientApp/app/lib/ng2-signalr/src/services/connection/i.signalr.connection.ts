import { Observable } from "rxjs/Observable";
import { SignalRConfiguration } from "../signalr.configuration";
import { BroadcastEventListener } from "../eventing/broadcast.event.listener";
import { ConnectionStatus } from "./connection.status";

export interface ISignalRConnection {
    readonly status: Observable<ConnectionStatus>;
    readonly errors: Observable<any>;
    readonly id: string;
    invoke(method: string, ...parameters: any[]): Promise<any>;
    listen<T>(listener: BroadcastEventListener<T>): void;
    listenFor<T>(listener: string): BroadcastEventListener<T>;
    stop(): void;
    start(): Promise<any>;
}
