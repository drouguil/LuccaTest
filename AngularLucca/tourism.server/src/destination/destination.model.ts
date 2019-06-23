import { IApiItem } from '../api';

export interface IDestination extends IApiItem {
	description: string;
	ratings: string;
	priceRange: string;
	tags: string[];
	gallery: string[];
	bg: string;
	thumbnail: string;
}
