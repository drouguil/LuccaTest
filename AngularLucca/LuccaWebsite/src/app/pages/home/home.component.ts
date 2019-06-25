import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/core/services/api.service';
import { Destination } from 'src/app/core/models/api/destination';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Sort } from 'src/app/core/models/sort';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { SortEnum } from 'src/app/core/models/sort-enum';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  /**
   * 
   */

  sorts: Sort[] = [
    new Sort('Nom', SortEnum.NAME, 'list'),
    new Sort('Prix', SortEnum.PRICE, 'euro'),
    new Sort('Note', SortEnum.RATING, 'star')
  ];

  /**
   * 
   */

  sortControl = new FormControl();

  /**
   * 
   */

  destinationControl = new FormControl();

  /**
   * 
   */

  destinationsOptions: Observable<Destination[]>;

  /**
   * 
   */

  destinations: Destination[];

  /**
   * 
   * @param apiService 
   * @param router 
   */

  constructor(
    private readonly apiService: ApiService,
    private readonly router: Router
  ) { }

  /**
   * 
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
   * 
   * @param destinationId 
   */

  selectDestination(destinationId: string) {
    this.router.navigate(['/destination/' + destinationId]);
  }

  /**
   * 
   * @param rating 
   */

  ratingArray(rating: string): any[] {
    return Array(Math.floor(+rating));
  }

  /**
   * 
   * @param rating 
   */

  isHalf(rating: string): boolean {
    const floor = Math.floor(+rating);
    return Math.round(+rating) - floor === 1;
  }

  /**
   * 
   */

  selectFilter() {
    const filter = this.sortControl.value as Sort;
    switch (filter.value) {
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
        console.error('Unknow filter', filter);
    }
  }

  /**
   * 
   * @param value 
   */

  private _filter(value: string): Destination[] {
    const filterValue = value.toLowerCase();

    return this.destinations.filter(destination => destination.name.toLowerCase().includes(filterValue));
  }

  /**
   * 
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
   * 
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
   * 
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
