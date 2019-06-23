import { IApiItem } from './api.model';

export abstract class AApiController<T extends IApiItem = IApiItem> {
	constructor(protected collection: Array<T>) {}
	getOne(req, res) {
		const id = req.params.id;
		const item = this.collection.find(d => d.id === id);
		if (!item) {
			res.status(404);
			res.send({ error: 'Not found' });
			return;
		}
		res.json(item);
	}
	getMany(req, res) {
		const query = req.query;
		const orderBy = query.orderBy;
		const paging = query.paging;
		const filters = query;
		delete filters.orderBy;
		delete filters.paging;

		let results = [...this.collection];

		Object.keys(filters).forEach(key => results = filterCollection(results, key, filters[key]));

		if (!!orderBy) {
			results = orderCollection(results, orderBy);
		}
		if (!!paging) {
			results = pageCollection(results, paging);
		}
		res.json(results);
	}
}

function pageCollection<T extends IApiItem = IApiItem>(collection: T[], paging: string) {
	const [offset, count] = paging.split(',');
	return collection.slice(+offset, +offset + +count);
}
function orderCollection<T extends IApiItem = IApiItem>(collection: T[], orderBy: string) {
	const [key, direction] = orderBy.split(',');
	const desc = direction === 'desc';
	return collection.sort((a: T, b: T) => {
		const aValue = a[key] as string;
		const bValue = b[key] as string;
		return (desc ? -1 : 1) * aValue.localeCompare(bValue);
	})
}
function filterCollection<T extends IApiItem = IApiItem>(collection: T[], key: string, filter: string) {
	const [k, operator] = key.split('$');
	const values = filter.split(',').map(v => v.toLowerCase());
	switch (operator) {
		case 'like':
			return likeFilter(collection, k, values);
		default:
			return valueFilter(collection, k, values);
	}
}

function valueFilter<T extends IApiItem = IApiItem>(collection: T[], key: string, values: string[]) {
	return collection.filter(item => values.includes(item[key].toLowerCase()));
}
function likeFilter<T extends IApiItem = IApiItem>(collection: T[], key: string, values: string[]) {
	return collection.filter(item => values.some(v => item[key].toLowerCase().includes(v)));

}