var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
import { Subject } from 'rxjs/Subject';
export var BroadcastEventListener = (function (_super) {
    __extends(BroadcastEventListener, _super);
    /**
     * @param {?} event
     */
    function BroadcastEventListener(event) {
        _super.call(this);
        this.event = event;
        if (event == null || event === '') {
            throw new Error('Failed to create BroadcastEventListener. Argument \'event\' can not be empty');
        }
    }
    return BroadcastEventListener;
}(Subject));
function BroadcastEventListener_tsickle_Closure_declarations() {
    /** @type {?} */
    BroadcastEventListener.prototype.event;
}
//# sourceMappingURL=broadcast.event.listener.js.map