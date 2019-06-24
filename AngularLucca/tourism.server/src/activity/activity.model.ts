import { IApiItem } from '../api';

export interface IActivity extends IApiItem {
	description: string;
	thumbnail: string;
	destinationId: string;
}
