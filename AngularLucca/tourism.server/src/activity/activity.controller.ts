
import { activities } from '../db';
import { AApiController } from '../api';
import { IActivity } from './activity.model';

export class ActivityController extends AApiController<IActivity> {
	constructor(collection = activities) { super(collection); }
}
