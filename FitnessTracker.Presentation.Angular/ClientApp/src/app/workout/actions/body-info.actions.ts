import { BodyInfo } from '../models';

export class GetBodyInfo {
    static readonly type = '[BodyInfo] GetBodyInfo';    
}

export class SaveBodyInfo {
    static readonly type = '[BodyInfo] SaveBodyInfo';

    constructor(public bodyInfo: BodyInfo) { }
}