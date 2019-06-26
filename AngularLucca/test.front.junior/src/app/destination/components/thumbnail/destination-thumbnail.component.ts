import { Component, Input } from '@angular/core';
import { IDestination } from '../../destination.model';

@Component({
	selector: 'app-destination-thumbnail',
	templateUrl: './destination-thumbnail.component.html',
	styleUrls: ['./destination-thumbnail.component.scss']
})
export class DestinationThumbnailComponent {
	@Input() destination: IDestination;
	get thumbnailImg() {
		return `assets/img/${this.destination.img || 'placeholder'}/thumbnail.jpg`;
	}
}
