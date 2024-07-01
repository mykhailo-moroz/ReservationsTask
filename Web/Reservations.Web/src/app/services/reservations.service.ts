import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs'

export interface Reservation {
  id: number;
  date: string;
  clientName: string;
}

@Injectable({
  providedIn: 'root'
})

export class ReservationsService {

  private apiUrl = 'http://localhost:5001/api/reservation'

  constructor(private http: HttpClient) { }

  getReservations(): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(this.apiUrl);
  }

  createReservation(reservation: Reservation): Observable<Reservation> {
    return this.http.post<Reservation>(this.apiUrl, reservation);
  }
}
