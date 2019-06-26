import { TestBed, inject } from '@angular/core/testing';

import { paris, marseille } from './destination.mock.spec';
import { DestinationService } from './destination.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('DestinationService', () => {
	let service: DestinationService;
	let httpCtrl: HttpTestingController;
	beforeEach(() => {
		TestBed.configureTestingModule({
			imports: [
				HttpClientTestingModule,
			],
			providers: [
				DestinationService,
			]
		});
		httpCtrl = TestBed.get(HttpTestingController);
	});
	beforeEach(inject([DestinationService], (_service: DestinationService) => {
		service = _service;
	}));
	it('should be created', () => {
		expect(service).toBeTruthy();
	});

	describe('searchDestinations', () => {
		it('should call http.get', () => {
			service.searchDestinations().subscribe(d => d);

			const req = httpCtrl.expectOne(r => r.url === '/api/destinations');
			expect(req.request.method).toEqual('GET');
			req.flush([]);
		});
		it('should call wtih just orderby if clue = ""', () => {
			service.searchDestinations().subscribe(d => d);

			const req = httpCtrl.expectOne(r => r.url === '/api/destinations');
			const params = req.request.params;
			expect(params.toString()).toEqual('orderBy=name');
			req.flush([]);
		});
		it('should call wtih filter on name if clue !=""', () => {
			const clue = 'asd';
			service.searchDestinations(clue).subscribe(d => d);

			const req = httpCtrl.expectOne(r => r.url === '/api/destinations');
			const params = req.request.params;
			expect(params.toString()).toEqual(`orderBy=name&name$like=${clue}`);
			req.flush([]);
		});
		it('should return the http result unscathed', () => {
			const clue = 'asd';
			const results = [paris, marseille];
			service.searchDestinations(clue).subscribe(d => {
				expect(d).toBe(results);
			});
			const req = httpCtrl.expectOne(r => r.url === '/api/destinations');
			req.flush(results);
		});

	});
	describe('getDestinationById', () => {
		it('should call http.get', () => {
			const id = '12';

			service.getDestinationById(id).subscribe(d => d);

			const req = httpCtrl.expectOne(`/api/destination/${id}`);
			expect(req.request.method).toEqual('GET');
			req.flush(paris);
		});
		it('should return the result unscathed', () => {
			const id = '12';

			service.getDestinationById(id).subscribe(d => {
				expect(d).toBe(paris);
			});

			const req = httpCtrl.expectOne(`/api/destination/${id}`);
			req.flush(paris);
		});
	});
});
