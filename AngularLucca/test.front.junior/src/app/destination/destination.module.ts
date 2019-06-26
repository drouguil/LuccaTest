import { NgModule } from '@angular/core';
import { DestinationService } from './destination.service';
import { DestinationRoutingModule } from './destination.router';
import { DestinationComponent } from './destination.component';
import { DestinationThumbnailComponent, DestinationGalleryComponent } from './components';
import { DestinationResolver } from './destination.resolver';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ApiModule } from '../api';
import { ActivityService, ActivityResolver } from '../activity';
import { ActivityThumbnailComponent } from '../activity/components';

@NgModule({
	imports: [
		DestinationRoutingModule,
		CommonModule,
		HttpClientModule,
		ApiModule,
	],
	providers: [
		ActivityService,
		ActivityResolver,
		DestinationService,
		DestinationResolver
	],
	declarations: [
		DestinationComponent,
		DestinationThumbnailComponent,
		DestinationGalleryComponent,
		ActivityThumbnailComponent
	],
	exports: [
		DestinationThumbnailComponent,
		ActivityThumbnailComponent
	]
})
export class DestinationModule {}
