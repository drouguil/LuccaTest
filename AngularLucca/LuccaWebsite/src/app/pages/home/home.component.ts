// Generic Angular

import { Component, OnInit } from '@angular/core';

// Forms

import { FormControl } from '@angular/forms';

// Routing

import { Router } from '@angular/router';

// RxJS

import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';

// Services

import { ApiService } from 'src/app/core/services';

// Models

import { Sort, SortEnum, Destination } from 'src/app/core/models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  /**
   * Array of sorts
   */

  sorts: Sort[] = [
    new Sort('Nom', SortEnum.NAME, 'list'),
    new Sort('Prix', SortEnum.PRICE, 'euro'),
    new Sort('Note', SortEnum.RATING, 'star')
  ];

  /**
   * Sort form control
   */

  sortControl = new FormControl();

  /**
   * Destination form control
   */

  destinationControl = new FormControl();

  /**
   * Destinations as observable
   */

  destinationsOptions: Observable<Destination[]>;

  /**
   * Destinations
   */

  destinations: Destination[];

  /**
   * Constructor
   * @param apiService API service
   * @param router Routing service
   */

  constructor(
    private readonly apiService: ApiService,
    private readonly router: Router
  ) { }

  /**
   * (async) Initialize form and get all destinations from API
   */

  async ngOnInit() {
    this.sortControl.setValue(this.sorts[0]);
    this.destinations = await this.apiService.getDestinations();
    this.destinationsOptions = this.destinationControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  /**
   * Select destination
   * @param destinationId destination Id
   */

  selectDestination(destinationId: string): void {
    this.router.navigate([`/destination/${destinationId}`]);
  }

  /**
   * Get array for *ngFor N times
   * @param rating Rating
   * @returns Array for rating foreach
   */

  ratingArray(rating: string): number[] {
    return Array(Math.floor(+rating));
  }

  /**
   * Check if rating is .5
   * @param rating rating
   * @returns If rating is .5
   */

  isHalf(rating: string): boolean {
    const floor = Math.floor(+rating);
    return Math.round(+rating) - floor === 1;
  }

  /**
   * Select sort from select
   */

  selectSort(): void {
    const sort = this.sortControl.value as Sort;
    switch (sort.value) {
      case SortEnum.NAME:
        this._sortByName();
        break;
      case SortEnum.PRICE:
        this._sortByPrice();
        break;
      case SortEnum.RATING:
        this._sortByRating();
        break;
      default:
        console.error('Unknow sort', sort);
    }
  }

  /**
   * Searching filter
   * @param value Searching value
   * @returns Destinations corresponding to filter
   */

  private _filter(value: string): Destination[] {
    const filterValue = value.toLowerCase();

    return this.destinations.filter(destination => destination.name.toLowerCase().includes(filterValue));
  }

  /**
   * Sort destinations by name ASC
   */

  private _sortByName(): void {
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

  /**
   * Sort destinations by price ASC
   */

  private _sortByPrice(): void {
    this.destinations = this.destinations.sort((a, b) => +a.priceRange.split('€')[0] - +b.priceRange.split('€')[0]);
    if (this.destinationControl.value) {
      this.destinationControl.updateValueAndValidity();
    } else {
      this.destinationControl.setValue('');
    }
  }

  /**
   * Sort destinations by rating DESC
   */

  private _sortByRating(): void {
    this.destinations = this.destinations.sort((a, b) => +b.rating - +a.rating);
    if (this.destinationControl.value) {
      this.destinationControl.updateValueAndValidity();
    } else {
      this.destinationControl.setValue('');
    }
  }

}
