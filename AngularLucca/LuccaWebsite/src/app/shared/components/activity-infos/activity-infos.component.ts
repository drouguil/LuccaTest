// Generic Angular

import { Component, Input } from '@angular/core';

// Models

import { Activity } from 'src/app/core/models';

@Component({
  selector: 'app-activity-infos',
  templateUrl: './activity-infos.component.html',
  styleUrls: ['./activity-infos.component.scss']
})
export class ActivityInfosComponent {

  /**
   * Activity from parent component
   */

  @Input() activity: Activity;

  constructor() { }

}
