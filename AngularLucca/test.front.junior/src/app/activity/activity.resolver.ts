import { Injectable } from '@angular/core';
import { ActivityService } from './activity.service';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { IActivity } from './activity.model';

@Injectable()
export class ActivityResolver implements Resolve<IActivity[]> {
	constructor(
		protected activityService: ActivityService,
	) {}
	resolve(route: ActivatedRouteSnapshot) {
		const id = route.paramMap.get('id');
		return this.activityService.getActivitiesByDestinationId(id);
	}
}
