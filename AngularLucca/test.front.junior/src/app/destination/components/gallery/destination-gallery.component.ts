import { Component, Input } from '@angular/core';
import { IDestination } from '../../destination.model';

@Component({
	selector: 'app-destination-gallery',
	templateUrl: './destination-gallery.component.html',
	styleUrls: ['./destination-gallery.component.scss']
})
export class DestinationGalleryComponent {
	@Input() destination: IDestination;
}
