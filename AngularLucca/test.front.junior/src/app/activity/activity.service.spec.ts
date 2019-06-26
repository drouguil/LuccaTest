import { TestBed, inject } from '@angular/core/testing';

import { ActivityService } from './activity.service';
import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';
import { kitesurf, fencing } from './activity.mock.spec';

describe('ActivityService', () => {
  let service: ActivityService;
  let httpCtrl: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
      ],
      providers: [
        ActivityService,
      ]
    });
    httpCtrl = TestBed.get(HttpTestingController);
  });
  beforeEach(inject([ActivityService], (_service: ActivityService) => {
    service = _service;
  }));
  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getActivitiesByDestinationId', () => {
    it('should call http.get', () => {
      const id = '5';

      service.getActivitiesByDestinationId(id).subscribe(d => d);

      const req = httpCtrl.expectOne(`/api/activities?destinationId=${id}`);
      expect(req.request.method).toEqual('GET');
      req.flush([kitesurf, fencing]);
    });
    it('should return the result unscathed', () => {
      const id = '5';

      service.getActivitiesByDestinationId(id).subscribe(d => {
        expect(d).toEqual([kitesurf, fencing]);
      });

      const req = httpCtrl.expectOne(`/api/activities?destinationId=${id}`);
      req.flush([kitesurf, fencing]);
    });
  });
});
