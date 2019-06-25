import { Component, OnInit, ElementRef, Renderer2 } from '@angular/core';
import { ApiService } from 'src/app/core/services/api.service';
import { Destination } from 'src/app/core/models/api/destination';
import { ActivatedRoute, Router } from '@angular/router';
import { Activity } from 'src/app/core/models/api/activity';

@Component({
  selector: 'app-destination',
  templateUrl: './destination.component.html',
  styleUrls: ['./destination.component.scss']
})
export class DestinationComponent implements OnInit {

  /**
   * 
   */

  destination: Destination;

  /**
   * 
   */

  activities: Activity[];

  /**
   * 
   * @param apiService 
   * @param route 
   * @param router 
   * @param elRef 
   * @param renderer 
   */

  constructor(
    private readonly apiService: ApiService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly elRef: ElementRef,
    private readonly renderer: Renderer2
  ) { }

  /**
   * 
   */

  async ngOnInit() {
    this.destination = await this.apiService.getDestinationById(this.route.snapshot.paramMap.get('id'));
    if (!this.destination) {
      this.router.navigate(['/home']);
    }
    this.activities = await this.apiService.getActivitiesByDestinationId(this.destination.id);
    this.renderer.setStyle(
      this.elRef.nativeElement,
      'background', 'url("' + this.destination.bg + '") center center no-repeat'
    );
  }

}
