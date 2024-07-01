import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReservationListComponent } from './components/reservation-list/reservation-list.component'
import { ReservationFormComponent } from './components/reservation-form/reservation-form.component'

const routes: Routes = [
  { path: 'reservation', component: ReservationListComponent },
  { path: 'add-reservation', component: ReservationFormComponent },
  { path: '', redirectTo: '/reservations', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
