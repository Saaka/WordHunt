import { Observable } from 'rxjs/Observable';
import { BroadcastEventListener } from '../eventing/broadcast.event.listener';
import { Subject } from 'rxjs/Subject';
import { ConnectionStatus } from '../connection/connection.status';
import { ISignalRConnection } from '../connection/i.signalr.connection';
export interface IListenerCollection {
    [name: string]: BroadcastEventListener<any>;
}
export declare class SignalRConnectionMock implements ISignalRConnection {
    private _mockErrors$;
    private _mockStatus$;
    private _listeners;
    constructor(_mockErrors$: Subject<any>, _mockStatus$: Subject<ConnectionStatus>, _listeners: IListenerCollection);
    readonly errors: Observable<any>;
    readonly status: Observable<ConnectionStatus>;
    readonly id: string;
    stop(): void;
    start(): Promise<any>;
    invoke(method: string, ...parameters: any[]): Promise<any>;
    listen<T>(listener: BroadcastEventListener<T>): void;
    listenFor<T>(event: string): BroadcastEventListener<T>;
}
