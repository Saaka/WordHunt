import { Subject } from 'rxjs/Subject';
import { SignalRConnectionMock, IListenerCollection } from './signalr.connection.mock';
import { ConnectionStatus } from '../connection/connection.status';
export declare class SignalRConnectionMockManager {
    private _status$;
    private _errors$;
    private _object;
    _listeners: IListenerCollection;
    constructor();
    readonly mock: SignalRConnectionMock;
    readonly errors$: Subject<any>;
    readonly status$: Subject<ConnectionStatus>;
    readonly listeners: IListenerCollection;
}
