import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Destination } from '../models/api/destination';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  public async getDestinations(): Promise<Destination[]> {
    try {
      return await this.http.get(environment.apiUrl + '/api/destinations?orderBy=name').toPromise() as Destination[];
    } catch (err) {
      console.error(err);
      return undefined;
    }
  }

  public async getDestinationById(id: string): Promise<Destination> {
    try {
      return await this.http.get(environment.apiUrl + '/api/destination/' + id).toPromise() as Destination;
    } catch (err) {
      console.error(err);
      return undefined;
    }
  }
}
