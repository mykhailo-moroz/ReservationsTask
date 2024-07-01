import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms'
import { ReservationsService, Reservation } from '../../services/reservations.service'

@Component({
  selector: 'app-reservation-form',
  templateUrl: './reservation-form.component.html',
  styleUrl: './reservation-form.component.css'
})

export class ReservationFormComponent{

  reservationForm: FormGroup;

  constructor(private fb: FormBuilder, private reservationsService: ReservationsService) {
    this.reservationForm = this.fb.group({
      date: ['', Validators.required],
      clientName: ['', Validators.requiredTrue]
    })
  }

  onSubmit(): void {
      const newReservation = this.reservationForm.value;
      this.reservationsService.createReservation(newReservation).subscribe();
  }
}
