import { ConnectionStatus } from './connection.status';
export declare class ConnectionStatuses {
    private static statuses;
    static readonly starting: ConnectionStatus;
    static readonly received: ConnectionStatus;
    static readonly connectionSlow: ConnectionStatus;
    static readonly reconnecting: ConnectionStatus;
    static readonly reconnected: ConnectionStatus;
    static readonly stateChanged: ConnectionStatus;
    static readonly disconnected: ConnectionStatus;
}
