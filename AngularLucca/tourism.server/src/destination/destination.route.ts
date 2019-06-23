import { DestinationController } from './destination.controller';


export function registerDestinationRoutes(app) {
	app.route('/api/destinations')
	.get((req, res) => {
		const ctrl = new DestinationController();
		return ctrl.getMany(req, res);
	});

	app.route('/api/destination/:id')
	.get((req, res) => {
		const ctrl = new DestinationController();
		return ctrl.getOne(req, res);
	});
}
