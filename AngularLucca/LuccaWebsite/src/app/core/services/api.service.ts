// Generic Angular

import { Injectable } from '@angular/core';

// Http

import { HttpClient } from '@angular/common/http';

// Environment

import { environment } from '../../../environments/environment';

// Models

import { Destination, Activity } from '../models';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  /**
   * Constructor
   * @param http Http requests manager
   */

  constructor(private http: HttpClient) { }

  /**
   * Get all destinations from api
   * @returns (async) All destinations
   */

  public async getDestinations(): Promise<Destination[]> {
    try {
      return await this.http.get(environment.apiUrl + '/api/destinations?orderBy=name').toPromise() as Destination[];
    } catch (err) {
      console.error(err);
      return undefined;
    }
  }

  /**
   * Get a destination with an id
   * @param id Id of the destination
   * @returns (async) Destination with the id
   */

  public async getDestinationById(id: string): Promise<Destination> {
    try {
      return await this.http.get(environment.apiUrl + `/api/destination/${id}`).toPromise() as Destination;
    } catch (err) {
      console.error(err);
      return undefined;
    }
  }

  /**
   * Get all activities with a destination id
   * @param id Destination id
   * @returns (async) Activy with the destination id
   */

  public async getActivitiesByDestinationId(id: string): Promise<Activity[]> {
    try {
      return await this.http.get(environment.apiUrl + `/api/activities?destinationId=${id}`).toPromise() as Activity[];
    } catch (err) {
      console.error(err);
      return undefined;
    }
  }
}
