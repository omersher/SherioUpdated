using System;

namespace Model
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        CheckedOut,
        Cancelled
    }

    public class Booking : BaseEntity
    {
        public int UserID { get; set; }
        public int RoomID { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                ValidateDates();
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                ValidateDates();
            }
        }

        public int AdultCount { get; set; }
        public int ChildCount { get; set; }

        // IMPORTANT: keep enum but stored as string in DB
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        private void ValidateDates()
        {
            if (startDate != default && endDate != default)
            {
                if (endDate <= startDate)
                    throw new ArgumentException("End date must be after start date");
            }
        }
    }

    public class BookingUpdateDto
    {
        public int Id { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public BookingStatus Status { get; set; }
    }
}