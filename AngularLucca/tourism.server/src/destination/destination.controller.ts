
import { destinations } from '../db';
import { AApiController } from '../api';
import { IDestination } from './destination.model';

export class DestinationController extends AApiController<IDestination> {
	constructor(collection = destinations) { super(collection); }
}
