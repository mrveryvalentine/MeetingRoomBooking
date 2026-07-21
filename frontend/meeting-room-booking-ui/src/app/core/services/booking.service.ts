import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Booking } from '../../models/booking';
import { CreateBookingRequest } from '../../models/create-booking-request';
import { UpdateBookingRequest } from '../../models/update-booking-request';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =`${environment.apiUrl}/bookings`;

  getBookings(): Observable<Booking[]> {
    return this.http.get<Booking[]>(this.apiUrl);
  }

  getBooking(id: number): Observable<Booking> {
    return this.http.get<Booking>(
      `${this.apiUrl}/${id}`);
  }

  createBooking(
    request: CreateBookingRequest
  ): Observable<Booking> {
    return this.http.post<Booking>(
      this.apiUrl,
      request);
  }

  updateBooking(
    id: number,
    request: UpdateBookingRequest
  ): Observable<void> {
    return this.http.put<void>(
      `${this.apiUrl}/${id}`,
      request);
  }

  deleteBooking(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${id}`);
  }
}