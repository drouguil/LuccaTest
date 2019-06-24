import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/core/services/api.service';
import { Destination } from 'src/app/core/models/api/destination';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-destination',
  templateUrl: './destination.component.html',
  styleUrls: ['./destination.component.scss']
})
export class DestinationComponent implements OnInit {

  destination: Destination;

  constructor(private readonly apiService: ApiService, private readonly route: ActivatedRoute, private readonly router: Router) { }

  async ngOnInit() {
    this.destination = await this.apiService.getDestinationById(this.route.snapshot.paramMap.get('id'));
    if (!this.destination) {
      this.router.navigate(['/home']);
    }
  }

}
