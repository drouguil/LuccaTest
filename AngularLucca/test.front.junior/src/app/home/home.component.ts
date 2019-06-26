import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable, of, merge } from 'rxjs';
import { debounceTime, switchMap, catchError } from 'rxjs/operators';
import { IDestination, DestinationService } from '../destination';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
	clueCtrl = new FormControl('');
	destinations$: Observable<IDestination[]>;
	constructor(
		protected destinationService: DestinationService,
	) {}
	ngOnInit() {
		this.destinations$ = merge(
			of(''),
			this.clueCtrl.valueChanges
		)
		.pipe(
			debounceTime(250),
			switchMap(clue => this.destinationService.searchDestinations(clue)),
			catchError(err => of([])),
		);
	}
}
