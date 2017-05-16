import { Subject } from 'rxjs/Subject';
export declare class BroadcastEventListener<T> extends Subject<T> {
    event: string;
    constructor(event: string);
}
