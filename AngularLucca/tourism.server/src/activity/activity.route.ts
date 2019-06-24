import { ActivityController } from './activity.controller';


export function registerActivityRoutes(app) {
	app.route('/api/activities')
	.get((req, res) => {
		const ctrl = new ActivityController();
		return ctrl.getMany(req, res);
	});

	app.route('/api/activity/:id')
	.get((req, res) => {
		const ctrl = new ActivityController();
		return ctrl.getOne(req, res);
	});
}
