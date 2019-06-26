import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home.router';
import { CommonModule } from '@angular/common';
import { DestinationModule } from '../destination';

@NgModule({
	imports: [
		HomeRoutingModule,
		ReactiveFormsModule,
		CommonModule,
		DestinationModule,
	],
	declarations: [
		HomeComponent,
	],
})
export class HomeModule {}
