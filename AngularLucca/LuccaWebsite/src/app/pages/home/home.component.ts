import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/core/services/api.service';
import { Destination } from 'src/app/core/models/api/destination';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Filter } from 'src/app/core/models/filter';
import { Observable, pipe } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { FilterEnum } from 'src/app/core/models/filer-enum';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  filters: Filter[] = [
    new Filter('Nom', FilterEnum.NAME, 'list'),
    new Filter('Prix', FilterEnum.PRICE, 'euro'),
    new Filter('Votes', FilterEnum.RATING, 'star')
  ];

  filterControl = new FormControl();

  destinationControl = new FormControl();

  destinationsOptions: Observable<Destination[]>;

  destinations: Destination[];

  constructor(private readonly apiService: ApiService, private readonly router: Router) {

  }

  async ngOnInit() {
    this.filterControl.setValue(this.filters[0]);
    this.destinations = await this.apiService.getDestinations();
    this.destinationsOptions = this.destinationControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  selectDestination(destinationId: string) {
    this.router.navigate(['/destination/' + destinationId]);
  }

  ratingArray(rating: string): any[] {
    return Array(Math.floor(+rating));
  }

  isHalf(rating: string): boolean {
    const floor = Math.floor(+rating);
    return Math.round(+rating) - floor === 1;
  }

  selectFilter() {
    const filter = this.filterControl.value as Filter;
    switch (filter.value) {
      case FilterEnum.NAME:
        this._filterByName();
        break;
      case FilterEnum.PRICE:
        this._filterByPrice();
        break;
      case FilterEnum.RATING:
        this._filterByRating();
        break;
      default:
        console.error('Unknow filter', filter);
    }
  }

  private _filter(value: string): Destination[] {
    const filterValue = value.toLowerCase();

    return this.destinations.filter(destination => destination.name.toLowerCase().includes(filterValue));
  }

  private _filterByName(): void {
    this.destinations = this.destinations
      .sort((a, b) => {
        if (a.name < b.name) { return -1; }
        if (a.name > b.name) { return 1; }
        return 0;
      });
    if (this.destinationControl.value) {
      this.destinationControl.updateValueAndValidity();
    } else {
      this.destinationControl.setValue('');
    }
  }

  private _filterByPrice(): void {
    this.destinations = this.destinations.sort((a, b) => +a.priceRange.split('€')[0] - +b.priceRange.split('€')[0]);
    if (this.destinationControl.value) {
      this.destinationControl.updateValueAndValidity();
    } else {
      this.destinationControl.setValue('');
    }
  }

  private _filterByRating(): void {
    this.destinations = this.destinations.sort((a, b) => +b.rating - +a.rating);
    if (this.destinationControl.value) {
      this.destinationControl.updateValueAndValidity();
    } else {
      this.destinationControl.setValue('');
    }
  }

}
