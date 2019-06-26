// Generic Angular

import { Component, OnInit, ElementRef, Renderer2 } from '@angular/core';

// Routing

import { ActivatedRoute, Router } from '@angular/router';

// Services

import { ApiService } from 'src/app/core/services';

// Models

import { Activity, Destination } from 'src/app/core/models';

@Component({
  selector: 'app-destination',
  templateUrl: './destination.component.html',
  styleUrls: ['./destination.component.scss']
})
export class DestinationComponent implements OnInit {

  /**
   * Destination
   */

  destination: Destination;

  /**
   * Activities of the destination
   */

  activities: Activity[];

  /**
   * Constructor
   * @param apiService API service
   * @param route Route service
   * @param router Routing service
   * @param elRef Root component element reference service
   * @param renderer Root component renderer
   */

  constructor(
    private readonly apiService: ApiService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly elRef: ElementRef,
    private readonly renderer: Renderer2
  ) { }

  /**
   * (async) Get all activities and the destination from API
   */

  async ngOnInit() {
    this.destination = await this.apiService.getDestinationById(this.route.snapshot.paramMap.get('id'));
    if (!this.destination) {
      this.router.navigate(['/home']);
    }
    this.activities = await this.apiService.getActivitiesByDestinationId(this.destination.id);
    this.renderer.setStyle(
      this.elRef.nativeElement,
      'background', `url("${this.destination.bg}") center center/cover no-repeat`
    );
  }

}
