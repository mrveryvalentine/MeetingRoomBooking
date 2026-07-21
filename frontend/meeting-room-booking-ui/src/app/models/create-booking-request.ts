export interface CreateBookingRequest {
  meetingTitle: string;
  organizer: string;
  roomId: number;
  startTime: string;
  endTime: string;
}