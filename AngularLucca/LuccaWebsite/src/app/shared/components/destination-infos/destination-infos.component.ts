import { Component, Input } from '@angular/core';
import { Destination } from 'src/app/core/models/api/destination';

@Component({
  selector: 'app-destination-infos',
  templateUrl: './destination-infos.component.html',
  styleUrls: ['./destination-infos.component.scss']
})
export class DestinationInfosComponent {

  /**
   * 
   */

  @Input() destination: Destination;

  constructor() { }

}
