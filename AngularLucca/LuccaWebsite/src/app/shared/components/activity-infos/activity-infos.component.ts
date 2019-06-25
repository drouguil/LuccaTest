import { Component, Input } from '@angular/core';
import { Activity } from 'src/app/core/models/api/activity';

@Component({
  selector: 'app-activity-infos',
  templateUrl: './activity-infos.component.html',
  styleUrls: ['./activity-infos.component.scss']
})
export class ActivityInfosComponent {

  /**
   * 
   */

  @Input() activity: Activity;

  constructor() { }

}
