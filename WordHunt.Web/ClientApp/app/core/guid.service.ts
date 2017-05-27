import { Injectable } from '@angular/core';

@Injectable()
export class GuidService {

    //TODO ADD REQUESTER TO REQUEST AND RESPONSES
    public create() {
        return this.createSegment() + this.createSegment() + '-' + this.createSegment() + '-' + this.createSegment() + '-' +
            this.createSegment() + '-' + this.createSegment() + this.createSegment() + this.createSegment();
    }

    private createSegment() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
}