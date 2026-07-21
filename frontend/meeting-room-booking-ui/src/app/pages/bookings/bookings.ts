import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { BookingService } from '../../core/services/booking.service';
import { RoomService } from '../../core/services/room.service';

import { Booking } from '../../models/booking';
import { Room } from '../../models/room';

@Component({
  selector: 'app-bookings',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './bookings.html',
  styleUrl: './bookings.scss'
})
export class Bookings implements OnInit {

  private readonly bookingService = inject(BookingService);
  private readonly roomService = inject(RoomService);
  private readonly fb = inject(FormBuilder);

  readonly bookings = signal<Booking[]>([]);
  readonly rooms = signal<Room[]>([]);

  readonly error = signal('');
  readonly saving = signal(false);
  readonly editingId = signal<number | null>(null);

  readonly form = this.fb.nonNullable.group({
    meetingTitle: ['', Validators.required],
    organizer: ['', Validators.required],
    roomId: [0, Validators.min(1)],
    startTime: ['', Validators.required],
    endTime: ['', Validators.required]
  });

  ngOnInit(): void {
    this.loadRooms();
    this.loadBookings();
  }

  onSubmit(): void {

    this.error.set('');

    const { startTime, endTime } = this.form.getRawValue();

    if (new Date(endTime) <= new Date(startTime)) {
      this.error.set('End time must be after start time.');
      return;
    }

    if (this.editingId() === null) {
      this.createBooking();
    } else {
      this.updateBooking();
    }

  }

  editBooking(booking: Booking): void {

    this.error.set('');

    this.editingId.set(booking.id);

    this.form.setValue({
      meetingTitle: booking.meetingTitle,
      organizer: booking.organizer,
      roomId: booking.roomId,
      startTime: booking.startTime.substring(0, 16),
      endTime: booking.endTime.substring(0, 16)
    });

  }

  cancelEdit(): void {

    this.editingId.set(null);
    this.error.set('');

    this.form.reset({
      meetingTitle: '',
      organizer: '',
      roomId: 0,
      startTime: '',
      endTime: ''
    });

  }

  deleteBooking(id: number): void {

    if (!confirm('Delete this booking?')) {
      return;
    }

    this.error.set('');

    this.bookingService.deleteBooking(id)
      .subscribe({

        next: () => {

          this.error.set('');
          this.loadBookings();

        },

        error: err => {

          if (err.status === 0) {

            this.error.set(
              'Cannot connect to the API. Please ensure the backend service is running.'
            );

          } else {

            this.error.set(
              err.error?.message ??
              'Unable to delete booking.'
            );

          }

        }

      });

  }

  private loadRooms(): void {

    this.error.set('');

    this.roomService.getRooms().subscribe({

      next: rooms => this.rooms.set(rooms),

      error: err => {

        if (err.status === 0) {

          this.error.set(
            'Cannot connect to the API. Please ensure the backend service is running.'
          );

        } else {

          this.error.set(
            err.error?.message ??
            'Unable to load meeting rooms.'
          );

        }

      }

    });

  }

  private loadBookings(): void {

    this.error.set('');

    this.bookingService.getBookings().subscribe({

      next: bookings => this.bookings.set(bookings),

      error: err => {

        if (err.status === 0) {

          this.error.set(
            'Cannot connect to the API. Please ensure the backend service is running.'
          );

        } else {

          this.error.set(
            err.error?.message ??
            'Unable to load bookings.'
          );

        }

      }

    });

  }
  private createBooking(): void {

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.error.set('');
    this.saving.set(true);

    this.bookingService.createBooking(this.form.getRawValue())
      .subscribe({

        next: () => this.afterSave(),

        error: err => {

          if (err.status === 0) {

              this.error.set(
                  'Cannot connect to the API. Please ensure the backend service is running.'
              );

          }
          else {

              this.error.set(
                  err.error?.message ??
                  'Unable to create booking.'
              );

          }

          this.saving.set(false);

        }

      });

  }

  private updateBooking(): void {

    if (this.form.invalid || this.editingId() === null) {
      this.form.markAllAsTouched();
      return;
    }

    this.error.set('');
    this.saving.set(true);

    this.bookingService.updateBooking(
      this.editingId()!,
      this.form.getRawValue()
    )
    .subscribe({

      next: () => this.afterSave(),

      error: err => {

        if (err.status === 0) {

          this.error.set(
            'Cannot connect to the API. Please ensure the backend service is running.'
          );

        } else {

          this.error.set(
            err.error?.message ??
            'Unable to update booking.'
          );

        }

        this.saving.set(false);

      }

    });

  }

  private afterSave(): void {

    this.saving.set(false);
    this.error.set('');
    this.editingId.set(null);

    this.loadBookings();

    this.form.reset({
      meetingTitle: '',
      organizer: '',
      roomId: 0,
      startTime: '',
      endTime: ''
    });

  }

}