// Generic Angular

import { Component, Input, OnInit } from '@angular/core';

// Models

import { Destination } from 'src/app/core/models';

@Component({
  selector: 'app-destination-infos',
  templateUrl: './destination-infos.component.html',
  styleUrls: ['./destination-infos.component.scss']
})
export class DestinationInfosComponent implements OnInit {

  /**
   * Destination from parent component
   */

  @Input() destination: Destination;

  /**
   * Array for rating
   */

  ratingArray: number[];

  /**
   * Array for not rating
   */

  notRatingArray: number[];

  /**
   * Constructor
   */

  constructor() { }

  /**
   * Initialize rating arrays
   */

  ngOnInit(): void {
    this.ratingArray = Array(Math.floor(+this.destination.rating));
    this.notRatingArray = Array(5 - this.ratingArray.length);
  }

}
