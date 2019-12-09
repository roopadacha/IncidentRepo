import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Incident } from '../models/incident';
import { IncidentType } from '../models/incidenttype';

@Injectable({
  providedIn: 'root'
})
export class IncidentService {

  clientUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient) {
      this.clientUrl = environment.appUrl + 'api/Incident/';
  }

  getIncidentTypes(): Observable<IncidentType[]>{
    return this.http.get<IncidentType[]>(environment.appUrl + 'api/IncidentType/')
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getIncidents(): Observable<Incident[]> {
    return this.http.get<Incident[]>(this.clientUrl)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getIncident(incidentId: number): Observable<Incident> {
      return this.http.get<Incident>(this.clientUrl + incidentId)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  saveIncident(incident): Observable<Incident> {
      return this.http.post<Incident>(this.clientUrl, JSON.stringify(incident), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  updateIncident(incidentId: number, incident): Observable<Incident> {
      return this.http.put<Incident>(this.clientUrl + incidentId, JSON.stringify(incident), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  deleteIncident(incidentId: number): Observable<Incident> {
      return this.http.delete<Incident>(this.clientUrl + incidentId)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}