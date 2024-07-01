import { Component, OnInit } from '@angular/core';
import { ReservationsService, Reservation } from '../../services/reservations.service'

@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrl: './reservation-list.component.css'
})
export class ReservationListComponent {

  reservations = new Array<Reservation>();

  constructor(private reservationsService: ReservationsService) { }


  ngOnInit(): void {
    this.reservationsService.getReservations().subscribe(data => {
      this.reservations = data;
    });
  }
}
