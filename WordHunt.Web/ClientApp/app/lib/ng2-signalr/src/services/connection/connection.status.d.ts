export declare class ConnectionStatus {
    private _name;
    readonly name: string;
    constructor(name: string);
    toString(): string;
    equals(other: ConnectionStatus): boolean;
}
