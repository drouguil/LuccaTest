import { Component, OnInit, Input } from '@angular/core';
import { IActivity } from '../../activity.model';

@Component({
  selector: 'app-activity-thumbnail',
  templateUrl: './activity-thumbnail.component.html',
  styleUrls: ['./activity-thumbnail.component.scss']
})
export class ActivityThumbnailComponent {
  @Input() activity: IActivity;
  constructor() { }
}
